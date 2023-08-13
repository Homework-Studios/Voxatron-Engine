package homeworkstudios.language.psi;

import com.intellij.psi.tree.IElementType;
import homeworkstudios.language.VoxaScriptLanguage;
import org.jetbrains.annotations.NonNls;
import org.jetbrains.annotations.NotNull;

public class VoxaScriptElementType extends IElementType {

    public VoxaScriptElementType(@NotNull @NonNls String debugName) {
        super(debugName, VoxaScriptLanguage.INSTANCE);
    }

}