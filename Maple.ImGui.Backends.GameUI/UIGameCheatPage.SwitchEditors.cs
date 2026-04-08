using Hexa.NET.ImGui;
using Maple.MonoGameAssistant.GameDTO;
using System.Globalization;
using System.Numerics;
using ImGuiApi = Hexa.NET.ImGui.ImGui;

namespace Maple.ImGui.Backends.GameUI
{
    /// <summary>
    /// 负责 Switch 类型展示项的编辑器渲染与交互处理。
    /// </summary>
    public partial class UIGameCheatPage
    {
        private static float GetSwitchDisplayEditorCardHeight(GameSwitchDisplayDTO attribute)
        {
            if (attribute.MultipleType)
            {
                var selectedContents = attribute.SelectedContents ?? [];
                return MathF.Max(108.0f, 72.0f + selectedContents.Count * 26.0f);
            }

            return 108.0f;
        }

        private void RenderSwitchDisplayEditor(GameSwitchDisplayDTO attribute, int index)
        {
            ImGuiApi.PushID($"{attribute.ObjectId ?? attribute.DisplayName ?? "Switch"}_{index}");
            var valueChanged = false;
            var originalContentValue = attribute.ContentValue;
            var originalDecimalValue = attribute.DecimalValue;
            var originalSwitchValue = attribute.SwitchValue;

            if (attribute.TextEditorType)
            {
                var editorKey = GetSwitchDisplayEditorKey(attribute, index);
                var decimalText = SwitchDisplayEditorTexts.TryGetValue(editorKey, out var cachedText)
                    ? cachedText
                    : attribute.DecimalValue.ToString(CultureInfo.InvariantCulture);
                decimalText = RenderStepInput("##DecimalValue", $"DecimalValue_{index}", decimalText, false);
                SwitchDisplayEditorTexts[editorKey] = decimalText;
                if (decimal.TryParse(decimalText, NumberStyles.Number, CultureInfo.InvariantCulture, out var decimalValue)
                    || decimal.TryParse(decimalText, NumberStyles.Number, CultureInfo.CurrentCulture, out decimalValue))
                {
                    if (attribute.DecimalValue != decimalValue)
                    {
                        attribute.DecimalValue = decimalValue;
                        valueChanged = true;
                    }
                }
            }
            else if (attribute.ButtonType)
            {
                const float actionButtonWidth = 88.0f;
                CenterSwitchEditorControl(actionButtonWidth);
                valueChanged = ImGuiApi.Button("Action", new Vector2(actionButtonWidth, 0.0f));
            }
            else if (attribute.SwitchesType)
            {
                var switchValue = attribute.SwitchValue;
                CenterSwitchEditorControl(40.0f);
                if (RenderSwitchToggle("##SwitchValue", ref switchValue))
                {
                    attribute.SwitchValue = switchValue;
                    valueChanged = true;
                }
            }
            else if (attribute.MultipleType)
            {
                valueChanged = RenderSwitchDisplayMultiSelectEditor(attribute);
            }
            else if (attribute.SelectsType)
            {
                valueChanged = RenderSwitchDisplaySelectEditor(attribute);
            }
            else
            {
                ImGuiApi.TextUnformatted(attribute.ContentValue ?? string.Empty);
            }

            if (valueChanged)
            {
                if (ShowCharacterStatusDialog && ViewingCharacterStatus is not null)
                {
                    HandleCharacterStatusValueChanged(attribute, originalContentValue, originalDecimalValue, originalSwitchValue);
                }
                else
                {
                    HandleSwitchDisplayValueChanged(attribute, originalContentValue, originalDecimalValue, originalSwitchValue);
                }
            }

            ImGuiApi.PopID();
        }

        private static bool RenderSwitchDisplayMultiSelectEditor(GameSwitchDisplayDTO attribute)
        {
            var selectedValues = ParseSelectedValues(attribute.ContentValue);
            var selectedContents = attribute.SelectedContents ?? [];
            var valueChanged = false;
            foreach (var option in selectedContents)
            {
                var optionKey = option.DisplayValue ?? string.Empty;
                var optionLabel = option.DisplayName ?? optionKey;
                var isSelected = selectedValues.Contains(optionKey);
                if (ImGuiApi.Checkbox(optionLabel, ref isSelected))
                {
                    if (isSelected)
                    {
                        selectedValues.Add(optionKey);
                    }
                    else
                    {
                        selectedValues.Remove(optionKey);
                    }

                    attribute.ContentValue = string.Join(',', selectedValues);
                    valueChanged = true;
                }
            }

            return valueChanged;
        }

        private static bool RenderSwitchDisplaySelectEditor(GameSwitchDisplayDTO attribute)
        {
            const float comboWidth = 120.0f;
            var selectedContents = attribute.SelectedContents ?? [];
            var previewValue = string.IsNullOrWhiteSpace(attribute.ContentValue) ? "Select..." : attribute.ContentValue;
            var valueChanged = false;
            foreach (var option in selectedContents)
            {
                if (string.Equals(option.DisplayValue, attribute.ContentValue, StringComparison.OrdinalIgnoreCase))
                {
                    previewValue = option.DisplayName ?? option.DisplayValue ?? string.Empty;
                    break;
                }
            }

            CenterSwitchEditorControl(comboWidth);
            ImGuiApi.SetNextItemWidth(comboWidth);
            if (!ImGuiApi.BeginCombo("##SelectContentValue", previewValue))
            {
                return false;
            }

            foreach (var option in selectedContents)
            {
                var optionKey = option.DisplayValue ?? string.Empty;
                var optionLabel = option.DisplayName ?? optionKey;
                var isSelected = string.Equals(attribute.ContentValue, optionKey, StringComparison.OrdinalIgnoreCase);
                if (ImGuiApi.Selectable(optionLabel, isSelected))
                {
                    attribute.ContentValue = optionKey;
                    valueChanged = true;
                }

                if (isSelected)
                {
                    ImGuiApi.SetItemDefaultFocus();
                }
            }

            ImGuiApi.EndCombo();
            return valueChanged;
        }

        private static HashSet<string> ParseSelectedValues(string? contentValue)
        {
            return [
                .. (contentValue ?? string.Empty)
                    .Split([',', ';', '|'], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries),
            ];
        }

        private static void CenterSwitchEditorControl(float width)
        {
            ImGuiApi.SetCursorPosX(ImGuiApi.GetCursorPosX() + MathF.Max(0.0f, (ImGuiApi.GetContentRegionAvail().X - width) * 0.5f));
        }

        private static string GetSwitchDisplayEditorKey(GameSwitchDisplayDTO attribute, int index)
        {
            return $"{attribute.ObjectId ?? attribute.DisplayName ?? "Switch"}_{index}";
        }

        private static bool RenderSwitchToggle(string id, ref bool value)
        {
            var size = SwitchToggleSize;
            var toggled = false;
            if (ImGuiApi.InvisibleButton(id, size))
            {
                value = !value;
                toggled = true;
            }

            var drawList = ImGuiApi.GetWindowDrawList();
            var min = ImGuiApi.GetItemRectMin();
            var max = ImGuiApi.GetItemRectMax();
            var radius = size.Y * 0.5f;
            var background = value ? SwitchToggleOnColor : SwitchToggleOffColor;
            drawList.AddRectFilled(min, max, ImGuiApi.ColorConvertFloat4ToU32(background), radius);
            drawList.AddCircleFilled(
                new Vector2(value ? max.X - radius : min.X + radius, min.Y + radius),
                radius - 3.0f,
                ImGuiApi.ColorConvertFloat4ToU32(new Vector4(1f, 1f, 1f, 1f)));
            return toggled;
        }
    }
}
