// This is a generated file. Not intended for manual editing.
package homeworkstudios.language.psi;

import com.intellij.psi.tree.IElementType;
import com.intellij.psi.PsiElement;
import com.intellij.lang.ASTNode;
import homeworkstudios.language.psi.impl.*;

public interface VoxaScriptTypes {

  IElementType PROPERTY = new VoxaScriptElementType("PROPERTY");

  IElementType COMMENT = new VoxaScriptTokenType("COMMENT");
  IElementType CRLF = new VoxaScriptTokenType("CRLF");
  IElementType KEY = new VoxaScriptTokenType("KEY");
  IElementType SEPARATOR = new VoxaScriptTokenType("SEPARATOR");
  IElementType VALUE = new VoxaScriptTokenType("VALUE");

  class Factory {
    public static PsiElement createElement(ASTNode node) {
      IElementType type = node.getElementType();
      if (type == PROPERTY) {
        return new VoxaScriptPropertyImpl(node);
      }
      throw new AssertionError("Unknown element type: " + type);
    }
  }
}
