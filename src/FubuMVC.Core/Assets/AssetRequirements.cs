using System.Collections.Generic;
using FubuMVC.Core.Content;
using System.Linq;

namespace FubuMVC.Core.Assets
{
    public class AssetRequirements
    {
        private readonly IContentFolderService _folders;
        private readonly AssetGraph _assetGraph;
        private readonly List<string> _requirements = new List<string>();
        private readonly List<string> _rendered = new List<string>();

        public AssetRequirements(IContentFolderService folders, AssetGraph assetGraph)
        {
            _folders = folders;
            _assetGraph = assetGraph;
        }

        public void Require(string name)
        {
            _requirements.Fill(name);
        }

        public IEnumerable<string> RequestedScripts()
        {
            return _requirements;
        }

        // TODO -- is this used?  If so, needs to be tied into AssetPipeline
        public void UseFileIfExists(string name)
        {
            if (_folders.FileExists(ContentType.scripts, name))
            {
                Require(name);
            }
        }

        /// <summary>
        /// Returns a list of scripts that have been Required, and any of their dependencies.
        /// </summary>
        /// <remarks>Can be called multiple times within an HTTP request, and will not return any script more than once.</remarks>
        /// <returns></returns>
        public IEnumerable<string> GetAssetsToRender()
        {
            var requiredAssets = _assetGraph.GetAssets(_requirements).Select(x => x.Name).Except(_rendered).ToList();
            _rendered.AddRange(requiredAssets);
            return requiredAssets;
        }
    }


   
}