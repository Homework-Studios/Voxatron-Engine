package homeworkstudios.language;

import com.intellij.psi.tree.TokenSet;
import homeworkstudios.language.psi.VoxaScriptTypes;

public interface VoxaScriptTokenSets {

    TokenSet IDENTIFIERS = TokenSet.create(VoxaScriptTypes.KEY);

    TokenSet COMMENTS = TokenSet.create(VoxaScriptTypes.COMMENT);

}