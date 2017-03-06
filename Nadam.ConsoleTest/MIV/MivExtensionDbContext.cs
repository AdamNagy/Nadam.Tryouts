using System.Collections.Generic;
using Nadam.Lib.JsonDb;

namespace Nadam.ConsoleTest.MIV
{
    class MivExtensionDbContext : JsonDbEngineContext
    {
        public MivExtensionDbContext(bool inmemory = true) : base("MivJsonDbExtension", inmemory)
        {
            if (inmemory)
            {
                _sequenceExtensions = GetTable<SequenceExtension>("SequenceExtensions") ?? new List<SequenceExtension>();
                _places = GetTable<Place>("Places") ?? new List<Place>();
                _highHeels = GetTable<HighHeel>("HighHeels") ?? new List<HighHeel>();
                _tightsTypes = GetTable<TightsType>("TightsTypes") ?? new List<TightsType>();
                _highHeelImages = GetTable<HighHeelImage>("HighHeelImages") ?? new List<HighHeelImage>();
            }
        }

        public IList<SequenceExtension> SequenceExtensions
        {
            get
            {
                if (!Inmemory)
                {
                    var value = GetTable<SequenceExtension>("SequenceExtensions");
                    return value ?? new List<SequenceExtension>();
                }
                return _sequenceExtensions;
            }
            set { _sequenceExtensions = value; }
        }
        private IList<SequenceExtension> _sequenceExtensions;


        public IList<Place> Places
        {
            get
            {
                if (!Inmemory)
                {
                    var value = GetTable<Place>("Places");
                    return value ?? new List<Place>();
                }
                return _places;
            }
            set { _places = value; }
        }
        private IList<Place> _places;


        public IList<HighHeel> HighHeels
        {
            get
            {
                if (!Inmemory)
                {
                    var value = GetTable<HighHeel>("HighHeels");
                    return value ?? new List<HighHeel>();
                }
                return _highHeels;
            }
            set { _highHeels = value; }
        }
        private IList<HighHeel> _highHeels;

        public IList<TightsType> TightsTypes
        {
            get
            {
                if (!Inmemory)
                {
                    var value = GetTable<TightsType>("TightsTypes");
                    return value ?? new List<TightsType>();
                }
                return _tightsTypes;
            }
            set { _tightsTypes = value; }
        }
        private IList<TightsType> _tightsTypes;


        public IList<HighHeelImage> HighHeelImages
        {
            get
            {
                if (!Inmemory)
                {
                    var value = GetTable<HighHeelImage>("HighHeelImages");
                    return value ?? new List<HighHeelImage>();
                }
                return _highHeelImages;
            }
            set { _highHeelImages = value; }
        }
        private IList<HighHeelImage> _highHeelImages;
    }
}
