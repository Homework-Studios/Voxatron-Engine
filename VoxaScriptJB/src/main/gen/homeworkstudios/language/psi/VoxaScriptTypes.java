// This is a generated file. Not intended for manual editing.
package homeworkstudios.language.psi;

import com.intellij.psi.tree.IElementType;
import com.intellij.psi.PsiElement;
import com.intellij.lang.ASTNode;
import homeworkstudios.language.psi.impl.*;

public interface VoxaScriptTypes {

  IElementType BLOCKEND = new VoxaScriptElementType("BLOCKEND");
  IElementType BLOCKSTART = new VoxaScriptElementType("BLOCKSTART");
  IElementType CODEBLOCK = new VoxaScriptElementType("CODEBLOCK");
  IElementType COMMA = new VoxaScriptElementType("COMMA");
  IElementType DEFAULT = new VoxaScriptElementType("DEFAULT");
  IElementType PROPERTY = new VoxaScriptElementType("PROPERTY");
  IElementType STRING = new VoxaScriptElementType("STRING");

  IElementType BLOCK_END = new VoxaScriptTokenType("BLOCK_END");
  IElementType BLOCK_START = new VoxaScriptTokenType("BLOCK_START");
  IElementType CODE_BLOCK = new VoxaScriptTokenType("CODE_BLOCK");
  IElementType COMMA_SEP = new VoxaScriptTokenType("COMMA_SEP");
  IElementType COMMENT = new VoxaScriptTokenType("COMMENT");
  IElementType CRLF = new VoxaScriptTokenType("CRLF");
  IElementType DEFAULT_FUN = new VoxaScriptTokenType("DEFAULT_FUN");
  IElementType KEY = new VoxaScriptTokenType("KEY");
  IElementType NUM = new VoxaScriptTokenType("NUM");
  IElementType SEPARATOR = new VoxaScriptTokenType("SEPARATOR");
  IElementType TEXT = new VoxaScriptTokenType("TEXT");
  IElementType VALUE = new VoxaScriptTokenType("VALUE");

  class Factory {
    public static PsiElement createElement(ASTNode node) {
      IElementType type = node.getElementType();
      if (type == BLOCKEND) {
        return new VoxaScriptBlockendImpl(node);
      }
      else if (type == BLOCKSTART) {
        return new VoxaScriptBlockstartImpl(node);
      }
      else if (type == CODEBLOCK) {
        return new VoxaScriptCodeblockImpl(node);
      }
      else if (type == COMMA) {
        return new VoxaScriptCommaImpl(node);
      }
      else if (type == DEFAULT) {
        return new VoxaScriptDefaultImpl(node);
      }
      else if (type == PROPERTY) {
        return new VoxaScriptPropertyImpl(node);
      }
      else if (type == STRING) {
        return new VoxaScriptStringImpl(node);
      }
      throw new AssertionError("Unknown element type: " + type);
    }
  }
}
