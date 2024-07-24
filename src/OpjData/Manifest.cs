using Newtonsoft.Json;
using Oxide.Patcher.Fields;
using Oxide.Patcher.Hooks;
using Oxide.Patcher.Modifiers;
using System.Collections.Generic;
using Oxide.Patcher.Common.JsonHelpers;

namespace Oxide.Patcher
{
    /// <summary>
    /// A set of changes to make to an assembly
    /// </summary>
    public class Manifest
    {
        /// <summary>
        /// Gets or sets the name of the assembly in the target directory
        /// </summary>
        public string AssemblyName { get; set; }

        /// <summary>
        /// When Enabled automatically tries to apply public modifiers to all members if it's considered to be safe.
        /// </summary>
        public bool AutoPublicModifiers { get; set; }

        /// <summary>
        /// Gets or sets the hooks contained in this project
        /// </summary>
        [JsonConverter(typeof(HooksListConverter))]
        public List<Hook> Hooks { get; set; } = new List<Hook>();

        /// <summary>
        /// Gets or sets the changed modifiers in this project
        /// </summary>
        public List<Modifier> Modifiers { get; set; } = new List<Modifier>();

        /// <summary>
        /// Gets or sets the additional fields in this project
        /// </summary>
        public List<Field> Fields { get; set; } = new List<Field>();
    }
}
