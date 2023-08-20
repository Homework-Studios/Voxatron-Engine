package homeworkstudios.language;

import com.intellij.openapi.editor.markup.TextAttributes;
import com.intellij.ui.JBColor;

public class VoxaScriptTextAttributes {

    public static final TextAttributes SEPARATOR = new TextAttributes();
    public static final TextAttributes NUM = new TextAttributes();
    public static final TextAttributes DEFAULT_FUN = new TextAttributes();
    public static final TextAttributes TEXT = new TextAttributes();
    public static final TextAttributes BRACES = new TextAttributes();
    public static final TextAttributes VAR_TOKEN = new TextAttributes();

    static void init() {
        SEPARATOR.setForegroundColor(new JBColor(0x000000, 0xffffff));
        NUM.setForegroundColor(new JBColor(0x000000, 0x2aacb8));
        DEFAULT_FUN.setForegroundColor(new JBColor(0x000000, 0xff9600));
        TEXT.setForegroundColor(new JBColor(0x000000, 0x6aab73));
        BRACES.setForegroundColor(new JBColor(0x000000, 0x00d8ff));
        VAR_TOKEN.setForegroundColor(new JBColor(0x000000, 0xFFC300));
    }
}
