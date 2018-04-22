namespace LISY.Entities.Documents
{
    /// <summary>
	/// Represents physical copies of documents.
	/// </summary>
	public class Copy
    {
        /// <summary>
        /// Id of a copy.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Id of a document of which copy is.
        /// </summary>
        public long DocumentId { get; set; }

        /// <summary>
        /// Id of a patron who took copy.
        /// </summary>
        public long PatronId { get; set; }

        /// <summary>
        /// Denotes if copy is taken.
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// Date when copy must be returned.
        /// </summary>
        public string ReturningDate { get; set; }

        /// <summary>
        /// Room where copy is placed.
        /// </summary>
        public int Room { get; set; }

        /// <summary>
        /// Level of copy room.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Is copy renewed or not.
        /// </summary>
        public bool IsRenewed { get; set; }

        /// <summary>
        /// Initializes a new instance of Copy.
        /// </summary>
        public Copy()
        {

        }        
    }
}
