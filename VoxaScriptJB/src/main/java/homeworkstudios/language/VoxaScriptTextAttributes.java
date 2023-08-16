package homeworkstudios.language;

import com.intellij.openapi.editor.markup.TextAttributes;
import com.intellij.ui.JBColor;

public class VoxaScriptTextAttributes {

    public static final TextAttributes SEPERATOR = new TextAttributes();
    public static final TextAttributes NUMBER = new TextAttributes();
    public static final TextAttributes DEFAULT_FUN = new TextAttributes();
    public static final TextAttributes TEXT = new TextAttributes();
    public static final TextAttributes BRACES = new TextAttributes();

    static void init() {
        SEPERATOR.setForegroundColor(new JBColor(0x000000, 0xffffff));
        NUMBER.setForegroundColor(new JBColor(0x000000, 0x2aacb8));
        DEFAULT_FUN.setForegroundColor(new JBColor(0x000000, 0xff9600));
        TEXT.setForegroundColor(new JBColor(0x000000, 0x6aab73));
        BRACES.setForegroundColor(new JBColor(0x000000, 0x00d8ff));
    }
}
