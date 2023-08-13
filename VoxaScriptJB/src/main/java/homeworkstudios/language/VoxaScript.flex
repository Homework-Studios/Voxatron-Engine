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
ARG_SEPARATOR={WHITE_SPACE}|","

LINE_COMMENT = \/\/[^\r\n]*
BLOCK_COMMENT = \/\*([^*]|\*[^/])*\*\/

%state WAITING_VALUE

%%

<YYINITIAL> {
{WHITE_SPACE}                                               { return TokenType.WHITE_SPACE; }
{LINE_COMMENT}/{EOL}                                        { return VoxaScriptTypes.COMMENT; }
{BLOCK_COMMENT}                                             { return VoxaScriptTypes.COMMENT; }
}
[^]                                                         { return TokenType.BAD_CHARACTER; }