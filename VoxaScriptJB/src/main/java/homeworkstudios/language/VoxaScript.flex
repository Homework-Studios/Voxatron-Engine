// Copyright 2000-2022 JetBrains s.r.o. and other contributors. Use of this source code is governed by the Apache 2.0 license that can be found in the LICENSE file.
package homeworkstudios.language;

import com.intellij.lexer.FlexLexer;
import com.intellij.psi.tree.IElementType;
import homeworkstudios.language.psi.VoxaScriptTypes;
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
MATH_OPERATOR===|[+\-*/%<>!&|\^]
NUM = [0-9]+
TEXT = \"([^\\\"]|\\.)*\"
COMMA_SEP = ","
SEMICOLON = ";"

EQUALS = "="
DEFAULT_FUN = "fun"|"function"
PRINT = "print"
VAR_TOKEN = "var"
VAR_CHARACTER=[^:=\ \n\t\f\\] | "\\ "

BLOCK_START = "{"
BLOCK_END = "}"
PARAM_START = "("
PARAM_END = ")"
CODE_BLOCK = "{[^}]*}"

LINE_COMMENT = \/\/[^\r\n]*
BLOCK_COMMENT = \/\*([^*]|\*[^/])*\*\/

%state WAITING_VALUE

%%

<YYINITIAL> {

{LINE_COMMENT}/{EOL}                                        { return VoxaScriptTypes.COMMENT; }
{BLOCK_COMMENT}                                             { return VoxaScriptTypes.COMMENT; }
{COMMA_SEP}                                                 { return VoxaScriptTypes.COMMA; }
{MATH_OPERATOR}                                             { return VoxaScriptTypes.MATH_OPERATOR; }
{DEFAULT_FUN}                                               { return VoxaScriptTypes.DEFAULT_FUN; }
      
{BLOCK_START}                                               { return VoxaScriptTypes.BLOCK_START; }
{BLOCK_END}                                                 { return VoxaScriptTypes.BLOCK_END; }
{CODE_BLOCK}                                                { return VoxaScriptTypes.CODE_BLOCK; }
{EQUALS}                                                    { return VoxaScriptTypes.EQUALS; }
{VAR_TOKEN}                                                 { return VoxaScriptTypes.VAR_TOKEN; }
{VAR_CHARACTER}+                                            { return VoxaScriptTypes.VAR_CHARACTER; }
/*
      {PARAM_START}                                               { return VoxaScriptTypes.PARAM_START; }
       {PARAM_END}                                                 { return VoxaScriptTypes.PARAM_END; }}


  */
{SEMICOLON}                                                 { return VoxaScriptTypes.SEMICOLON; }
           {NUM}                                                       { return VoxaScriptTypes.NUM; }
      {TEXT}                                                      { return VoxaScriptTypes.TEXT; }

      {WHITE_SPACE}                                               { return TokenType.WHITE_SPACE; }
}
[^]                                                         { return TokenType.BAD_CHARACTER; }