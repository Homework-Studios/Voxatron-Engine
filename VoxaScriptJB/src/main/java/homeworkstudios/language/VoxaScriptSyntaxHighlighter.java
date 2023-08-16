package homeworkstudios.language;

import com.intellij.lexer.Lexer;
import com.intellij.openapi.editor.DefaultLanguageHighlighterColors;
import com.intellij.openapi.editor.HighlighterColors;
import com.intellij.openapi.editor.colors.TextAttributesKey;
import com.intellij.openapi.fileTypes.SyntaxHighlighterBase;
import com.intellij.psi.TokenType;
import com.intellij.psi.tree.IElementType;
import homeworkstudios.language.psi.VoxaScriptTypes;
import org.jetbrains.annotations.NotNull;

import static com.intellij.openapi.editor.colors.TextAttributesKey.createTextAttributesKey;

@SuppressWarnings("deprecation")
public class VoxaScriptSyntaxHighlighter extends SyntaxHighlighterBase {

    public static final TextAttributesKey SEPARATOR =
            createTextAttributesKey("VoxaScript_SEPARATOR", VoxaScriptTextAttributes.SEPARATOR);
    public static final TextAttributesKey COMMENT =
            createTextAttributesKey("VoxaScript_COMMENT", DefaultLanguageHighlighterColors.LINE_COMMENT);
    public static final TextAttributesKey BAD_CHARACTER =
            createTextAttributesKey("VoxaScript_BAD_CHARACTER", HighlighterColors.BAD_CHARACTER);
    public static final TextAttributesKey DEFAULT_FUN =
            createTextAttributesKey("VoxaScript_DEFAULT_FUN", VoxaScriptTextAttributes.DEFAULT_FUN);
    public static final TextAttributesKey TEXT =
            createTextAttributesKey("VoxaScript_TEXT", VoxaScriptTextAttributes.TEXT);
    public static final TextAttributesKey BLOCK_START =
            createTextAttributesKey("VoxaScript_BLOCK_START", VoxaScriptTextAttributes.BRACES);
    public static final TextAttributesKey BLOCK_END =
            createTextAttributesKey("VoxaScript_BLOCK_END", VoxaScriptTextAttributes.BRACES);
    public static final TextAttributesKey NUM =
            createTextAttributesKey("VoxaScript_NUM", VoxaScriptTextAttributes.NUM);

    private static final TextAttributesKey[] BAD_CHAR_KEYS = new TextAttributesKey[]{BAD_CHARACTER};
    private static final TextAttributesKey[] SEPARATOR_KEYS = new TextAttributesKey[]{SEPARATOR};
    private static final TextAttributesKey[] COMMENT_KEYS = new TextAttributesKey[]{COMMENT};
    private static final TextAttributesKey[] DEFAULT_FUN_KEYS = new TextAttributesKey[]{DEFAULT_FUN};
    private static final TextAttributesKey[] TEXT_KEYS = new TextAttributesKey[]{TEXT};
    private static final TextAttributesKey[] BLOCK_KEYS = new TextAttributesKey[]{BLOCK_END, BLOCK_START};
    private static final TextAttributesKey[] NUM_KEYS = new TextAttributesKey[]{NUM};
    private static final TextAttributesKey[] EMPTY_KEYS = new TextAttributesKey[0];

    @NotNull
    @Override
    public Lexer getHighlightingLexer() {
        return new VoxaScriptLexerAdapter();
    }

    @Override
    public TextAttributesKey @NotNull [] getTokenHighlights(IElementType tokenType) {
        if (tokenType.equals(VoxaScriptTypes.SEPARATOR)) {
            return SEPARATOR_KEYS;
        }
        if (tokenType.equals(VoxaScriptTypes.COMMENT)) {
            return COMMENT_KEYS;
        }
        if (tokenType.equals(TokenType.BAD_CHARACTER)) {
            return BAD_CHAR_KEYS;
        }
        if (tokenType.equals(VoxaScriptTypes.DEFAULT_FUN)) {
            return DEFAULT_FUN_KEYS;
        }
        if (tokenType.equals(VoxaScriptTypes.TEXT)) {
            return TEXT_KEYS;
        }
        if (tokenType.equals(VoxaScriptTypes.BLOCK_START) || tokenType.equals(VoxaScriptTypes.BLOCK_END)) {
            return BLOCK_KEYS;
        }
        if (tokenType.equals(VoxaScriptTypes.NUM)) {
            return NUM_KEYS;
        }
        return EMPTY_KEYS;
    }

}