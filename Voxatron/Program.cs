using Voxatron_Engine;
using Voxatron.Scene;

VoxatronEngine engine = new();
engine.AttachScene(new TestScene());
engine.Run();