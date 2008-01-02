using System;

namespace KentState.Purple
{
    public enum PurpleMessageFlags
    {
	    Send        = 0x0001, /**< Outgoing message.        */
	    Recv        = 0x0002, /**< Incoming message.        */
	    System      = 0x0004, /**< System message.          */
	    Auto_Resp   = 0x0008, /**< Auto response.           */
	    Active_Only = 0x0010, /**< Hint to the UI that this
	                               message should not be
	                               shown in conversations
	                               which are only open for
	                               internal UI purposes
	                               (e.g. for contact-aware
	                               conversions).           */
	    Nick        = 0x0020, /**< Contains your nick.      */
	    No_Log      = 0x0040, /**< Do not log.              */
	    Whisper     = 0x0080, /**< Whispered message.       */
	    Error       = 0x0200, /**< Error message.           */
	    Delayed     = 0x0400, /**< Delayed message.         */
	    Raw         = 0x0800, /**< "Raw" message - don't
	                               apply formatting         */
        Images      = 0x1000, /**< Message contains images  */
        Notify      = 0x2000, /**< Message is a notification */
        Linkify     = 0x4000, /**< Message should not be auto-
				    			   linkified */
	    Invisible   = 0x8000, /**< Message should not be displayed */
    }
}
