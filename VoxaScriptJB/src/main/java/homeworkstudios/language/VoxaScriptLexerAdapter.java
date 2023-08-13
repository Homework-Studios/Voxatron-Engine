package homeworkstudios.language;

import com.intellij.lexer.FlexAdapter;

public class VoxaScriptLexerAdapter extends FlexAdapter {

    public VoxaScriptLexerAdapter() {
        super(new VoxaScriptLexer(null));
    }

}