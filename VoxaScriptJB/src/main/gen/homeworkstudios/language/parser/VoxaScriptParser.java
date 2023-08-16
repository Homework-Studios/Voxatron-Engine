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
    return VoxaScriptFile(b, l + 1);
  }

  /* ********************************************************** */
  // item_*
  static boolean VoxaScriptFile(PsiBuilder b, int l) {
    if (!recursion_guard_(b, l, "VoxaScriptFile")) return false;
    while (true) {
      int c = current_position_(b);
      if (!item_(b, l + 1)) break;
      if (!empty_element_parsed_guard_(b, "VoxaScriptFile", c)) break;
    }
    return true;
  }

  /* ********************************************************** */
  // BLOCK_END
  public static boolean blockend(PsiBuilder b, int l) {
    if (!recursion_guard_(b, l, "blockend")) return false;
    if (!nextTokenIs(b, BLOCK_END)) return false;
    boolean r;
    Marker m = enter_section_(b);
    r = consumeToken(b, BLOCK_END);
    exit_section_(b, m, BLOCKEND, r);
    return r;
  }

  /* ********************************************************** */
  // BLOCK_START
  public static boolean blockstart(PsiBuilder b, int l) {
    if (!recursion_guard_(b, l, "blockstart")) return false;
    if (!nextTokenIs(b, BLOCK_START)) return false;
    boolean r;
    Marker m = enter_section_(b);
    r = consumeToken(b, BLOCK_START);
    exit_section_(b, m, BLOCKSTART, r);
    return r;
  }

  /* ********************************************************** */
  // CODE_BLOCK
  public static boolean codeblock(PsiBuilder b, int l) {
    if (!recursion_guard_(b, l, "codeblock")) return false;
    if (!nextTokenIs(b, CODE_BLOCK)) return false;
    boolean r;
    Marker m = enter_section_(b);
    r = consumeToken(b, CODE_BLOCK);
    exit_section_(b, m, CODEBLOCK, r);
    return r;
  }

  /* ********************************************************** */
  // COMMA_SEP
  public static boolean comma(PsiBuilder b, int l) {
    if (!recursion_guard_(b, l, "comma")) return false;
    if (!nextTokenIs(b, COMMA_SEP)) return false;
    boolean r;
    Marker m = enter_section_(b);
    r = consumeToken(b, COMMA_SEP);
    exit_section_(b, m, COMMA, r);
    return r;
  }

  /* ********************************************************** */
  // DEFAULT_FUN
  public static boolean default_$(PsiBuilder b, int l) {
    if (!recursion_guard_(b, l, "default_$")) return false;
    if (!nextTokenIs(b, DEFAULT_FUN)) return false;
    boolean r;
    Marker m = enter_section_(b);
    r = consumeToken(b, DEFAULT_FUN);
    exit_section_(b, m, DEFAULT, r);
    return r;
  }

  /* ********************************************************** */
  // property|COMMENT|CRLF
  static boolean item_(PsiBuilder b, int l) {
    if (!recursion_guard_(b, l, "item_")) return false;
    boolean r;
    r = property(b, l + 1);
    if (!r) r = consumeToken(b, COMMENT);
    if (!r) r = consumeToken(b, CRLF);
    return r;
  }

  /* ********************************************************** */
  // NUM
  public static boolean number(PsiBuilder b, int l) {
    if (!recursion_guard_(b, l, "number")) return false;
    if (!nextTokenIs(b, NUM)) return false;
    boolean r;
    Marker m = enter_section_(b);
    r = consumeToken(b, NUM);
    exit_section_(b, m, NUMBER, r);
    return r;
  }

  /* ********************************************************** */
  // (KEY? SEPARATOR VALUE?) | KEY
  public static boolean property(PsiBuilder b, int l) {
    if (!recursion_guard_(b, l, "property")) return false;
    if (!nextTokenIs(b, "<property>", KEY, SEPARATOR)) return false;
    boolean r;
    Marker m = enter_section_(b, l, _NONE_, PROPERTY, "<property>");
    r = property_0(b, l + 1);
    if (!r) r = consumeToken(b, KEY);
    exit_section_(b, l, m, r, false, null);
    return r;
  }

  // KEY? SEPARATOR VALUE?
  private static boolean property_0(PsiBuilder b, int l) {
    if (!recursion_guard_(b, l, "property_0")) return false;
    boolean r;
    Marker m = enter_section_(b);
    r = property_0_0(b, l + 1);
    r = r && consumeToken(b, SEPARATOR);
    r = r && property_0_2(b, l + 1);
    exit_section_(b, m, null, r);
    return r;
  }

  // KEY?
  private static boolean property_0_0(PsiBuilder b, int l) {
    if (!recursion_guard_(b, l, "property_0_0")) return false;
    consumeToken(b, KEY);
    return true;
  }

  // VALUE?
  private static boolean property_0_2(PsiBuilder b, int l) {
    if (!recursion_guard_(b, l, "property_0_2")) return false;
    consumeToken(b, VALUE);
    return true;
  }

  /* ********************************************************** */
  // TEXT
  public static boolean string(PsiBuilder b, int l) {
    if (!recursion_guard_(b, l, "string")) return false;
    if (!nextTokenIs(b, TEXT)) return false;
    boolean r;
    Marker m = enter_section_(b);
    r = consumeToken(b, TEXT);
    exit_section_(b, m, STRING, r);
    return r;
  }

}
