// This is a generated file. Not intended for manual editing.
package homeworkstudios.language.parser;

import com.intellij.lang.PsiBuilder;
import com.intellij.lang.PsiBuilder.Marker;
import static homeworkstudios.language.psi.VoxaScriptTypes.*;
import static com.intellij.lang.parser.GeneratedParserUtilBase.*;
import com.intellij.psi.tree.IElementType;
import com.intellij.lang.ASTNode;
import com.intellij.psi.tree.TokenSet;
import com.intellij.lang.PsiParser;
import com.intellij.lang.LightPsiParser;

@SuppressWarnings({"SimplifiableIfStatement", "UnusedAssignment"})
public class VoxaScriptParser implements PsiParser, LightPsiParser {

  public ASTNode parse(IElementType t, PsiBuilder b) {
    parseLight(t, b);
    return b.getTreeBuilt();
  }

  public void parseLight(IElementType t, PsiBuilder b) {
    boolean r;
    b = adapt_builder_(t, b, this, null);
    Marker m = enter_section_(b, 0, _COLLAPSE_, null);
    r = parse_root_(t, b);
    exit_section_(b, 0, m, t, r, true, TRUE_CONDITION);
  }

  protected boolean parse_root_(IElementType t, PsiBuilder b) {
    return parse_root_(t, b, 0);
  }

  static boolean parse_root_(IElementType t, PsiBuilder b, int l) {
    return simpleFile(b, l + 1);
  }

  /* ********************************************************** */
  // var|COMMENT|CRLF|FUNCTION
  static boolean item_(PsiBuilder b, int l) {
    if (!recursion_guard_(b, l, "item_")) return false;
    boolean r;
    r = var(b, l + 1);
    if (!r) r = consumeToken(b, COMMENT);
    if (!r) r = consumeToken(b, CRLF);
    if (!r) r = consumeToken(b, FUNCTION);
    return r;
  }

  /* ********************************************************** */
  // item_*
  static boolean simpleFile(PsiBuilder b, int l) {
    if (!recursion_guard_(b, l, "simpleFile")) return false;
    while (true) {
      int c = current_position_(b);
      if (!item_(b, l + 1)) break;
      if (!empty_element_parsed_guard_(b, "simpleFile", c)) break;
    }
    return true;
  }

  /* ********************************************************** */
  // (VAR KEY? SEPARATOR VALUE?) | KEY
  public static boolean var(PsiBuilder b, int l) {
    if (!recursion_guard_(b, l, "var")) return false;
    if (!nextTokenIs(b, "<var>", KEY, VAR)) return false;
    boolean r;
    Marker m = enter_section_(b, l, _NONE_, VAR, "<var>");
    r = var_0(b, l + 1);
    if (!r) r = consumeToken(b, KEY);
    exit_section_(b, l, m, r, false, null);
    return r;
  }

  // VAR KEY? SEPARATOR VALUE?
  private static boolean var_0(PsiBuilder b, int l) {
    if (!recursion_guard_(b, l, "var_0")) return false;
    boolean r;
    Marker m = enter_section_(b);
    r = consumeToken(b, VAR);
    r = r && var_0_1(b, l + 1);
    r = r && consumeToken(b, SEPARATOR);
    r = r && var_0_3(b, l + 1);
    exit_section_(b, m, null, r);
    return r;
  }

  // KEY?
  private static boolean var_0_1(PsiBuilder b, int l) {
    if (!recursion_guard_(b, l, "var_0_1")) return false;
    consumeToken(b, KEY);
    return true;
  }

  // VALUE?
  private static boolean var_0_3(PsiBuilder b, int l) {
    if (!recursion_guard_(b, l, "var_0_3")) return false;
    consumeToken(b, VALUE);
    return true;
  }

}
