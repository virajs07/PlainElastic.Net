using PlainElastic.Net.Utils;

namespace PlainElastic.Net.Queries
{
    /// <summary>
	/// A single-value metrics aggregation that calculates an approximate count of distinct values.
	/// Values can be extracted either from specific fields in the document or generated by a script.
	/// see http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-metrics-cardinality-aggregation.html
	/// </summary>
	public class CardinalityAggregation<T> : ValueAggregationBase<CardinalityAggregation<T>, T>
    {
		/// <summary>
		/// The precision_threshold options allows to trade memory for accuracy, and defines a unique count below which counts are expected to be close to accurate.
		/// Above this value, counts might become a bit more fuzzy. The maximum supported value is 40000, thresholds above this number will have the same effect as a threshold of 40000.
		/// Default value depends on the number of parent aggregations that multiple create buckets (such as terms or histograms).
		/// </summary>
		public CardinalityAggregation<T> PrecisionThreshold(int precisionThreshold)
		{
            RegisterJsonPart("'precision_threshold': {0}", precisionThreshold.AsString());
			return this;
		}

		/// <summary>
		/// If you computed a hash on client-side, stored it into your documents and want Elasticsearch to use them to compute counts using this hash function without rehashing values,
		/// it is possible to specify rehash: false. Default value is true. Please note that the hash must be indexed as a long when rehash is false.
		/// </summary>
		public CardinalityAggregation<T> Rehash(bool rehash)
		{
			RegisterJsonPart("'rehash': {0}", rehash.AsString());
			return this;
		}


        protected override string ApplyAggregationBodyJsonTemplate(string body)
        {
            return "'cardinality': {{ {0} }}".AltQuoteF(body);
        }
        
    }
}