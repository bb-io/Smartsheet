using Apps.Smartsheet.Actions;
using Tests.Smartsheet.Base;

namespace Tests.Smartsheet;

[TestClass]
public class ActionTests : TestBase
{
    [TestMethod]
    public async Task Dynamic_handler_works()
    {
        var actions = new Actions(InvocationContext);

        await actions.Action();
    }
}
