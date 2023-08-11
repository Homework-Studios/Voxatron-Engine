package homeworkstudios.files;

import com.intellij.lang.Language;

public class VoxascriptLanguage extends Language {

    public static final VoxascriptLanguage INSTANCE = new VoxascriptLanguage();

    private VoxascriptLanguage() {
        super("VoxaScript");
    }

}