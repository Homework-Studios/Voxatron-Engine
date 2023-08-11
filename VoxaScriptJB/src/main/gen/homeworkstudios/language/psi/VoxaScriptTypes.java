// This is a generated file. Not intended for manual editing.
package homeworkstudios.language.psi;

import com.intellij.lang.ASTNode;
import com.intellij.psi.PsiElement;
import com.intellij.psi.tree.IElementType;
import homeworkstudios.grammar.VoxaScriptElementType;
import homeworkstudios.grammar.VoxaScriptTokenType;
import homeworkstudios.language.psi.impl.VoxaScriptVarImpl;

public interface VoxaScriptTypes {

    IElementType VAR = new VoxaScriptElementType("VAR");

    IElementType COMMENT = new VoxaScriptTokenType("COMMENT");
    IElementType CRLF = new VoxaScriptTokenType("CRLF");
    IElementType FUNCTION = new VoxaScriptTokenType("FUNCTION");
    IElementType KEY = new VoxaScriptTokenType("KEY");
    IElementType SEPARATOR = new VoxaScriptTokenType("SEPARATOR");
    IElementType VALUE = new VoxaScriptTokenType("VALUE");

    class Factory {
        public static PsiElement createElement(ASTNode node) {
            IElementType type = node.getElementType();
            if (type == VAR) {
                return new VoxaScriptVarImpl(node);
            }
            throw new AssertionError("Unknown element type: " + type);
        }
    }
}
