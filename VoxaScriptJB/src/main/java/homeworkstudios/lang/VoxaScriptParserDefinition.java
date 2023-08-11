package homeworkstudios.lang;

import com.intellij.lang.ASTNode;
import com.intellij.lang.ParserDefinition;
import com.intellij.lang.PsiParser;
import com.intellij.lexer.Lexer;
import com.intellij.openapi.project.Project;
import com.intellij.psi.FileViewProvider;
import com.intellij.psi.PsiElement;
import com.intellij.psi.PsiFile;
import com.intellij.psi.tree.IFileElementType;
import com.intellij.psi.tree.TokenSet;
import homeworkstudios.files.VoxascriptLanguage;
import homeworkstudios.language.parser.VoxaScriptParser;
import homeworkstudios.language.psi.VoxaScriptTypes;
import org.jetbrains.annotations.NotNull;

public class VoxaScriptParserDefinition implements ParserDefinition {

    public static final IFileElementType FILE = new IFileElementType(VoxascriptLanguage.INSTANCE);

    @NotNull
    @Override
    public Lexer createLexer(Project project) {
        return new VoxaScriptLexerAdapter();
    }

    @NotNull
    @Override
    public TokenSet getCommentTokens() {
        return VoxaScriptTokenSet.COMMENTS;
    }

    @NotNull
    @Override
    public TokenSet getStringLiteralElements() {
        return TokenSet.EMPTY;
    }

    @NotNull
    @Override
    public PsiParser createParser(final Project project) {
        return new VoxaScriptParser();
    }

    @NotNull
    @Override
    public IFileElementType getFileNodeType() {
        return FILE;
    }

    @NotNull
    @Override
    public PsiFile createFile(@NotNull FileViewProvider viewProvider) {
        return new VoxaScriptFile(viewProvider);
    }

    @NotNull
    @Override
    public PsiElement createElement(ASTNode node) {
        return VoxaScriptTypes.Factory.createElement(node);
    }

}