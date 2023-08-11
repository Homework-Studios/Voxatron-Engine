package homeworkstudios.lang;

import com.intellij.psi.tree.TokenSet;
import homeworkstudios.language.psi.VoxaScriptTypes;


public interface VoxaScriptTokenSet {
    TokenSet VARIABLE = TokenSet.create(VoxaScriptTypes.VAR);

    TokenSet IDENTIFIERS = TokenSet.create(VoxaScriptTypes.KEY);

    TokenSet COMMENTS = TokenSet.create(VoxaScriptTypes.COMMENT);
}
