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
  IElementType NUMBER = new VoxaScriptElementType("NUMBER");
  IElementType PARAMEND = new VoxaScriptElementType("PARAMEND");
  IElementType PARAMSTART = new VoxaScriptElementType("PARAMSTART");
  IElementType PROPERTY = new VoxaScriptElementType("PROPERTY");
  IElementType STRING = new VoxaScriptElementType("STRING");
  IElementType VAR = new VoxaScriptElementType("VAR");
  IElementType VOXA_SCRIPT_FILE = new VoxaScriptElementType("VOXA_SCRIPT_FILE");

  IElementType BLOCK_END = new VoxaScriptTokenType("BLOCK_END");
  IElementType BLOCK_START = new VoxaScriptTokenType("BLOCK_START");
  IElementType CODE_BLOCK = new VoxaScriptTokenType("CODE_BLOCK");
  IElementType COMMA_SEP = new VoxaScriptTokenType("COMMA_SEP");
  IElementType COMMENT = new VoxaScriptTokenType("COMMENT");
  IElementType CRLF = new VoxaScriptTokenType("CRLF");
  IElementType DEFAULT_FUN = new VoxaScriptTokenType("DEFAULT_FUN");
  IElementType EQALS = new VoxaScriptTokenType("EQALS");
  IElementType KEY = new VoxaScriptTokenType("KEY");
  IElementType NUM = new VoxaScriptTokenType("NUM");
  IElementType PARAM_END = new VoxaScriptTokenType("PARAM_END");
  IElementType PARAM_START = new VoxaScriptTokenType("PARAM_START");
  IElementType SEMICOLON = new VoxaScriptTokenType("SEMICOLON");
  IElementType SEPARATOR = new VoxaScriptTokenType("SEPARATOR");
  IElementType TEXT = new VoxaScriptTokenType("TEXT");
  IElementType VALUE = new VoxaScriptTokenType("VALUE");
  IElementType VAR_TOKEN = new VoxaScriptTokenType("VAR_TOKEN");
  IElementType WHITE_SPACE = new VoxaScriptTokenType("WHITE_SPACE");

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
      else if (type == NUMBER) {
        return new VoxaScriptNumberImpl(node);
      }
      else if (type == PARAMEND) {
        return new VoxaScriptParamendImpl(node);
      }
      else if (type == PARAMSTART) {
        return new VoxaScriptParamstartImpl(node);
      }
      else if (type == PROPERTY) {
        return new VoxaScriptPropertyImpl(node);
      }
      else if (type == STRING) {
        return new VoxaScriptStringImpl(node);
      }
      else if (type == VAR) {
        return new VoxaScriptVarImpl(node);
      }
      else if (type == VOXA_SCRIPT_FILE) {
        return new VoxaScriptVoxaScriptFileImpl(node);
      }
      throw new AssertionError("Unknown element type: " + type);
    }
  }
}
