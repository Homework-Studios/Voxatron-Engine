package homeworkstudios.language;

import com.intellij.psi.tree.IElementType;
import com.intellij.psi.TokenType;
import java.util.LinkedList;
%%

%{
  boolean isConditionExpression = false;
  // Stolen from Mathematica support plugin. This adds support for nested states.
  private final LinkedList<Integer> states = new LinkedList();

  private void yypushstate(int state) {
      states.addFirst(yystate());
      yybegin(state);
  }
  private void yypopstate() {
      final int state = states.removeFirst();
      yybegin(state);
  }
%}

%public
%class VoxaScriptLexer
%implements FlexLexer
%function advance
%type IElementType
%unicode
%ignorecase

EOL= (\r|\n|\r\n|\f)
LINE_WS=[\ \t]
WHITE_SPACE=({LINE_WS}|{EOL})+
ARG_SEPARATOR={WHITE_SPACE}|","

LINE_COMMENT = \/\/[^\r\n]*
BLOCK_COMMENT = \/\*([^*]|\*[^/])*\*\/

STRING_RAW = (\'\'\') ~ (\'\'\')
STRING = \' ([^\']|\\\')* \'

NUMBER = -?[0-9]+ | 0o[0-7]+ | 0b[0-1]+ | 0x[0-9a-fA-F]+

IDENTIFIER = [A-Za-z_][A-Za-z0-9_]*

AROPS = \+ | \- | \* | \/ | \%
ASSIGN = (\+|-)?=
EQ = == | \!= | (< | >)=?




%state IN_ARGLIST

%%

<YYINITIAL> {
{WHITE_SPACE}                { return TokenType.WHITE_SPACE; }
"var"                        { return VoxaScriptTypes.VAR; }
{LINE_COMMENT}/{EOL}         { return VoxaScriptTypes.COMMENT; }
{BLOCK_COMMENT}              { return VoxaScriptTypes.COMMENT; }
/*

  "("                      { return VoxaScriptTypes.LPAR; }
  ")"                      { return VoxaScriptTypes.RPAR; }
  "["                      { return VoxaScriptTypes.LSQUAREBRACKET; }
  "]"                      { return VoxaScriptTypes.RSQUAREBRACKET; }
  "{"                      { return VoxaScriptTypes.LCURL; }
  "}"                      { return VoxaScriptTypes.RCURL; }
  ":"                      { return VoxaScriptTypes.COLON; }
  ";"                      { return VoxaScriptTypes.SEMICOLON; }
  ","                      { return VoxaScriptTypes.COMMA; }
  "."                      { return VoxaScriptTypes.DOT; }
  "?"                      { return VoxaScriptTypes.QMARK; }
  {EQ}                     { return VoxaScriptTypes.EQ; }

  "if"                     { return VoxaScriptTypes.IF; }
  "true"                   { return VoxaScriptTypes.TRUE; }
  "false"                  { return VoxaScriptTypes.FALSE; }
  "break"                  { return VoxaScriptTypes.BREAK; }
  "continue"               { return VoxaScriptTypes.CONTINUE; }

  "print"                  { return VoxaScriptTypes.PRINT; }


  "="                      { return VoxaScriptTypes.ASSIGN; }


  {IDENTIFIER}             { return VoxaScriptTypes.IDENTIFIER; }
  {BLOCK_COMMENT}          { return VoxaScriptTypes.COMMENT; }
  {STRING_RAW}             { return VoxaScriptTypes.STRING_RAW; }
  {STRING}                 { return VoxaScriptTypes.STRING; }
  {NUMBER}                 { return VoxaScriptTypes.NUMBER; }
  {ARG_SEPARATOR}+         { yybegin(IN_ARGLIST); return TokenType.WHITE_SPACE; }
  {AROPS}                  { return VoxaScriptTypes.ARITHMETIC_OPERATOR; }
*/

//  [^\ \t\r\n\'0-9]+       { return UNRECOGNIZED; }
}

/*
<IN_ARGLIST> {
  {ARG_SEPARATOR}+          { yybegin(IN_ARGLIST); return com.intellij.psi.TokenType.WHITE_SPACE; }

  "("                      { yypushstate(IN_ARGLIST); return LPAR; }
  ")"                      { yypopstate(); return RPAR; }

  "true"                   { return TRUE; }
  "false"                  { return FALSE; }
  ":"                      { return COLON; }


}
*/

[^] { return TokenType.BAD_CHARACTER; }