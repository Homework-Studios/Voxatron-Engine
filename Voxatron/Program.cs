using Voxatron_Engine;
using Voxatron.Scene;

const bool testMode = false;

VoxatronEngine engine = new("Voxatron");

if (testMode)
{
    engine.AttachScene(new TestScene());
}
else
{
    engine.AttachScene(new TitleScene());
}

engine.Run();