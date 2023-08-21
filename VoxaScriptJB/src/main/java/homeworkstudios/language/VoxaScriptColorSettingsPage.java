package homeworkstudios.language;

import com.intellij.openapi.editor.colors.TextAttributesKey;
import com.intellij.openapi.fileTypes.SyntaxHighlighter;
import com.intellij.openapi.options.colors.AttributesDescriptor;
import com.intellij.openapi.options.colors.ColorDescriptor;
import com.intellij.openapi.options.colors.ColorSettingsPage;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;

import javax.swing.*;
import java.util.Map;

public class VoxaScriptColorSettingsPage implements ColorSettingsPage {

    private static final AttributesDescriptor[] DESCRIPTORS = new AttributesDescriptor[]{
            new AttributesDescriptor("NUM", VoxaScriptSyntaxHighlighter.NUM),
            new AttributesDescriptor("MATH_OPERATOR", VoxaScriptSyntaxHighlighter.MATH_OPERATOR),
            new AttributesDescriptor("Bad value", VoxaScriptSyntaxHighlighter.BAD_CHARACTER)
    };

    @Nullable
    @Override
    public Icon getIcon() {
        return VoxaScriptIcons.FILE;
    }

    @NotNull
    @Override
    public SyntaxHighlighter getHighlighter() {
        return new VoxaScriptSyntaxHighlighter();
    }

    @NotNull
    @Override
    public String getDemoText() {
        return VoxaScriptDemoTextHolder.DEMO_TEXT;
    }

    @Nullable
    @Override
    public Map<String, TextAttributesKey> getAdditionalHighlightingTagToDescriptorMap() {
        return null;
    }

    @Override
    public AttributesDescriptor @NotNull [] getAttributeDescriptors() {
        return DESCRIPTORS;
    }

    @Override
    public ColorDescriptor @NotNull [] getColorDescriptors() {
        return ColorDescriptor.EMPTY_ARRAY;
    }

    @NotNull
    @Override
    public String getDisplayName() {
        return "VoxaScript";
    }

}