package homeworkstudios.lang;

import com.intellij.lexer.FlexAdapter;
import homeworkstudios.language.VoxaScriptLexer;

public class VoxaScriptLexerAdapter extends FlexAdapter {

    public VoxaScriptLexerAdapter() {
        super(new VoxaScriptLexer(null));
    }

}
