package homeworkstudios.highlighting;

import com.intellij.openapi.editor.colors.TextAttributesKey;
import com.intellij.openapi.fileTypes.SyntaxHighlighter;
import com.intellij.openapi.options.colors.AttributesDescriptor;
import com.intellij.openapi.options.colors.ColorDescriptor;
import com.intellij.openapi.options.colors.ColorSettingsPage;
import homeworkstudios.files.VoxaScriptIcons;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;

import javax.swing.*;
import java.util.Map;

public class VoxaScriptColorSettingsPage implements ColorSettingsPage {

    private static final AttributesDescriptor[] DESCRIPTORS = new AttributesDescriptor[]{
            new AttributesDescriptor("Operators//Plus", VoxaScriptSyntaxHighlighter.PLUS),
            new AttributesDescriptor("Operators//Minus", VoxaScriptSyntaxHighlighter.MINUS),
            new AttributesDescriptor("Operators", VoxaScriptSyntaxHighlighter.OPERATOR),
            new AttributesDescriptor("Key", VoxaScriptSyntaxHighlighter.KEY),
            new AttributesDescriptor("Separator", VoxaScriptSyntaxHighlighter.SEPARATOR),
            new AttributesDescriptor("Value", VoxaScriptSyntaxHighlighter.VALUE),
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
        return "print(\"Script Tests running...\");\n" +
                "\n" +
                "if 1 + 1 == 2 {\n" +
                "    print(\"1 + 1 == 2\");\n" +
                "}\n" +
                "\n" +
                "/*\n" +
                "switch 1 + 1 {\n" +
                "    \n" +
                "    case 2 {\n" +
                "        print(\"1 + 1 == 2\");\n" +
                "    }\n" +
                "    \n" +
                "    default {\n" +
                "        print(\"1 + 1 != 2\");\n" +
                "    }\n" +
                "}\n" +
                "\n" +
                "\n" +
                "var i = 0;\n" +
                "\n" +
                "while i < 10 {\n" +
                "    print(i);\n" +
                "    i = i + 1;\n" +
                "}\n" +
                "\n" +
                "for i in 0 -> 10 {\n" +
                "    print(i);\n" +
                "}\n" +
                "*/\n" +
                "\n" +
                "function test() {\n" +
                "    print(\"test\");\n" +
                "}\n" +
                "\n" +
                "test();\n" +
                "\n" +
                "function testTwo(a, b) {\n" +
                "    print(a + b);\n" +
                "}\n" +
                "\n" +
                "testTwo(1, 2);\n" +
                "\n" +
                "\n" +
                "function testThree(a, b) {\n" +
                "    return a + b;\n" +
                "}\n" +
                "\n" +
                "print(testThree(1,2));\n" +
                "\n";
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