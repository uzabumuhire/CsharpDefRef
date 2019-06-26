using System.Dynamic;

using static Core.Utility;

namespace Advanced.DynamicBinding.Basics
{
    /// <summary>
    /// Uses custom binding to intercept and interprest all method calls.
    /// </summary>
    class Duck : DynamicObject
    {
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            DisplayInfo(binder.Name + " method was called.");
            result = null;
            return true;
        }
    }
}
