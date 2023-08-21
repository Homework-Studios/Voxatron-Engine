package homeworkstudios.language.psi.impl;

import com.intellij.lang.ASTNode;
import com.intellij.navigation.ItemPresentation;
import com.intellij.psi.PsiElement;
import com.intellij.psi.PsiFile;
import homeworkstudios.language.psi.VoxaScriptElementFactory;
import homeworkstudios.language.psi.VoxaScriptProperty;
import homeworkstudios.language.psi.VoxaScriptTypes;
import org.jetbrains.annotations.Nullable;

import javax.swing.*;

public class VoxaScriptPsiImplUtil {

    public static String getKey(VoxaScriptProperty element) {
        ASTNode keyNode = element.getNode().findChildByType(VoxaScriptTypes.VAR_CHARACTER);
        if (keyNode != null) {
            // IMPORTANT: Convert embedded escaped spaces to VoxaScript spaces
            return keyNode.getText().replaceAll("\\\\ ", " ");
        } else {


            return null;
        }
    }

    public static String getName(VoxaScriptProperty element) {
        return getKey(element);
    }

    public static PsiElement setName(VoxaScriptProperty element, String newName) {
        ASTNode keyNode = element.getNode().findChildByType(VoxaScriptTypes.VAR_CHARACTER);
        if (keyNode != null) {
            VoxaScriptProperty property =
                    VoxaScriptElementFactory.createProperty(element.getProject(), newName);
            ASTNode newKeyNode = property.getFirstChild().getNode();
            element.getNode().replaceChild(keyNode, newKeyNode);
        }
        return element;
    }

    public static PsiElement getNameIdentifier(VoxaScriptProperty element) {
        ASTNode keyNode = element.getNode().findChildByType(VoxaScriptTypes.VAR_CHARACTER);
        return keyNode != null ? keyNode.getPsi() : null;
    }

    public static String getValue(VoxaScriptProperty element) {
        ASTNode valueNode = element.getNode().findChildByType(VoxaScriptTypes.VALUE);
        if (valueNode != null) {
            return valueNode.getText();
        } else {
            return null;
        }
    }

    public static ItemPresentation getPresentation(final VoxaScriptProperty element) {
        return new ItemPresentation() {
            @Nullable
            @Override
            public String getPresentableText() {
                return element.getKey();
            }

            @Nullable
            @Override
            public String getLocationString() {
                PsiFile containingFile = element.getContainingFile();
                return containingFile == null ? null : containingFile.getName();
            }

            @Override
            public Icon getIcon(boolean unused) {
                return element.getIcon(0);
            }
        };
    }
}