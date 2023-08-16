package homeworkstudios.language

import com.intellij.formatting.*
import com.intellij.lang.ASTNode
import com.intellij.psi.TokenType
import com.intellij.psi.formatter.common.AbstractBlock
import com.intellij.psi.tree.IElementType
import homeworkstudios.language.psi.VoxaScriptTypes

class VoxaScriptBlock(node: ASTNode, wrap: Wrap?, alignment: Alignment?) : AbstractBlock(node, wrap, alignment) {
    override fun getIndent(): Indent {
        val elementType = myNode.elementType
        val element = myNode.psi
        val parentElement = element.parent

        if (parentElement is VoxaScriptFile) {
            return Indent.getNoneIndent()
        }

        if (element is VoxaScriptNestedStatement) {
            return Indent.getNormalIndent()
        }


        return Indent.getNoneIndent()
    }

    override fun getSpacing(child1: Block?, child2: Block): Spacing? {
        return null//TODO
    }

    override fun isLeaf(): Boolean {
        return myNode.firstChildNode == null
    }

    override fun buildChildren(): List<Block> {
        if (isLeaf) return emptyList()
        return buildSubBlocks()
    }

    private fun buildSubBlocks(): List<Block> {
        val blocks = mutableListOf<Block>()
        var child = myNode.firstChildNode
        while (child != null) {
            val skip = child.textRange.length == 0 || child.elementType === TokenType.WHITE_SPACE
            if (!skip) {
                blocks.add(VoxaScriptBlock(child, null, null))
            }
            child = child.treeNext
        }
        return blocks
    }

    override fun getChildAttributes(newChildIndex: Int): ChildAttributes {
        val subBlocks = subBlocks
        fun isBraceSubBlock(index: Int, braceElementType: IElementType): Boolean {
            if (index >= 0 && index < subBlocks.size) {
                val subBlock = subBlocks[index]
                if (subBlock.isLeaf && subBlock.textRange.length == 1) {
                    val node = myNode.findLeafElementAt(subBlock.textRange.startOffset - myNode.startOffset)
                    if (node?.elementType == braceElementType) {
                        return true
                    }
                }
            }
            return false
        }
        // Indent insertion after '{' and before '}'
        if (isBraceSubBlock(newChildIndex - 1, VoxaScriptTypes.BLOCK_START) || isBraceSubBlock(newChildIndex, VoxaScriptTypes.BLOCK_END)) {
            return ChildAttributes(Indent.getNormalIndent(), null)
        }
        // Indent insertion after '}' (most rules end with 'ws')
        if (newChildIndex == subBlocks.size && isBraceSubBlock(newChildIndex - 1, VoxaScriptTypes.BLOCK_END)) {
            return ChildAttributes(Indent.getNoneIndent(), null)
        }


        return super.getChildAttributes(newChildIndex)
    }

    override fun getChildIndent(): Indent? {
        //TODO: do not duplicate logic from getIndent
        val element = myNode.psi

        if (element is VoxaScriptFile) {
            return Indent.getNoneIndent()
        }

        return super.getChildIndent()
    }
}