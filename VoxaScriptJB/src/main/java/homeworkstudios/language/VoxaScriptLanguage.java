package homeworkstudios.language;

import com.intellij.lang.Language;

public class VoxaScriptLanguage extends Language {

    public static final VoxaScriptLanguage INSTANCE = new VoxaScriptLanguage();

    private VoxaScriptLanguage() {
        super("VoxaScript");
        VoxaScriptTextAttributes.init();
    }

}