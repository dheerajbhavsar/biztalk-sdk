//---------------------------------------------------------------------
// File: FixMsgStream.cs
// 
// Summary: A Sample of how to write a custom pipeline component.
//
// Sample: Custom Pipeline Component SDK 
//
//---------------------------------------------------------------------
// This file is part of the Microsoft BizTalk Server SDK
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
// This source code is intended only as a supplement to Microsoft BizTalk
// Server 2016 release and/or on-line documentation. See these other
// materials for detailed information regarding Microsoft code samples.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
// KIND, WHETHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
// PURPOSE.
//---------------------------------------------------------------------

namespace Microsoft.Samples.BizTalk.Pipelines.CustomComponent
{
	using System;
	using System.IO;
	using System.Resources;

	/// <summary>
	/// Implements a wrapper for System.IO.Stream class.
	/// </summary>
	/// <remarks>
	/// FizMsgStream derives from System.IO.Stream class to 
	/// enable streaming processing of messages in BizTalk receive and send 
	/// pipelines.
	/// </remarks>
	public class FixMsgStream : Stream
	{
		private Stream	stm			= null;
		
		private byte[]	prependData = null;
		private byte[]	appendData	= null;

		private ResourceManager resourceManager = null;

		private int		prependDataOffset	= 0;
		private int		appendDataOffset	= 0;

		/// <summary>
		/// FixMsgStream constructor.
		/// </summary>
		/// <param name="stm">Stream object.</param>
		/// <param name="prependData">Bytes to prepend at the beginning of stream.</param>
		/// <param name="appendData">Bytes to append at the end of stream.</param>
		public FixMsgStream(Stream stm, byte[] prependData, byte[] appendData, ResourceManager resManager)
		{
			this.stm = stm;
			this.prependData = prependData;
			this.appendData = appendData;
			this.resourceManager = resManager;
		}
		
		/// <summary>
		/// One can read from the stream.
		/// </summary>
		override public bool CanRead
		{
			get { return true; }
		}

		/// <summary>
		/// One cannot write to the stream.
		/// </summary>
		override public bool CanWrite
		{
			get { return false; }
		}

		/// <summary>
		/// One cannot seek the stream.
		/// </summary>
		override public bool CanSeek
		{
			get { return false; }
		}

		/// <summary>
		/// Flushes the stream (not supported).
		/// </summary>
		override public void Flush()
		{
			throw new NotSupportedException(resourceManager.GetString("ErrorFlushNotSupported"));
		}

		/// <summary>
		/// Closes the stream.
		/// </summary>
		override public void Close()
		{
			if (stm != null)
			{
				stm.Close();
				stm = null;
			}
		}

		/// <summary>
		/// Gets the length of the stream (Not supported).
		/// </summary>
		override public long Length
		{
			get { throw new NotSupportedException(resourceManager.GetString("ErrorLengthNotSupported")); }
		}

		/// <summary>
		/// Gets the current read position in the stream. Setting of position is not supported.
		/// </summary>
		override public long Position
		{
			get { return stm.Position + prependDataOffset + appendDataOffset; }
			set { throw new NotSupportedException(resourceManager.GetString("ErrorPosSetNotSupported")); }
		}

		/// <summary>
		/// Seeks within the stream (also used for retrieving the current position within the stream).
		/// </summary>
		/// <param name="pos">Seek position.</param>
		/// <param name="origin">Seek origin.</param>
		/// <returns>New position in the stream.</returns>
		override public long Seek(long offset, SeekOrigin origin)
		{
			long pos = -1;

			switch(origin)
			{
				case SeekOrigin.Begin :
					pos = offset;
					break;
				case SeekOrigin.Current :
					pos = Position + offset;
					break;
				case SeekOrigin.End :
					break;
			}

			// We generally disallow seeking of the stream
			// However in unmanaged world, Seek(0,CURR) is used to retrieve current position
			// We'll special case this: in other words, if seek does not change position, we'll not throw exception
			if (pos==Position)
			{
				return pos;
			}
			else
			{
				throw new NotSupportedException(resourceManager.GetString("ErrorSeekNotSupported"));
			}
		}

		/// <summary>
		/// Sets the length of the stream (Not supported).
		/// </summary>
		/// <param name="len">New length.</param>
		override public void SetLength(long len)
		{
			throw new NotSupportedException(resourceManager.GetString("ErrorSetLengthNotSupported"));
		}

		/// <summary>
		/// Writes to the stream (Not supported).
		/// </summary>
		/// <param name="buffer">Data buffer</param>
		/// <param name="offset">Offset</param>
		/// <param name="count">Count of bytes to write</param>
		override public void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException(resourceManager.GetString("ErrorWriteNotSupported"));
		}

		/// <summary>
		/// Reads from the stream.
		/// </summary>
		/// <param name="buffer">An array of bytes.</param>
		/// <param name="offset">The zero-based byte offset in buffer.</param>
		/// <param name="count">The maximum number of bytes to be read.</param>
		/// <returns>The total number of bytes read into the buffer.</returns>
		/// <remarks>
		/// This method reads a sequence of bytes from the 
		/// current stream and appends and/or prepends data to
		/// the stream. When this method returns, the buffer contains 
		/// the specified byte array with the values between offset 
		/// and (offset + count) replaced by the bytes read from 
		/// the current source. 
		/// </remarks>
		override public int Read(byte[] buffer, int offset, int count)
		{
			int ret = 0;
			int bytesRead = 0;

			if (prependData!=null)
			{
				bytesRead = count > prependData.Length ? prependData.Length : count;
				Array.Copy(prependData, prependDataOffset, buffer, offset, bytesRead);

				if (bytesRead==prependData.Length)
					prependData = null;	// prependData consumed
				
				prependDataOffset	+= bytesRead;
				offset				+= bytesRead;
				count				-= bytesRead;
				ret					+= bytesRead;
			}

			bytesRead	 = stm.Read(buffer, offset, count);
			offset		+= bytesRead;
			count		-= bytesRead;
			ret			+= bytesRead;

			if ((bytesRead==0) && (appendData!=null))
			{
				// End of stream
				bytesRead = count > appendData.Length ? appendData.Length : count;
				Array.Copy(appendData, appendDataOffset, buffer, offset, bytesRead);

				if (bytesRead==appendData.Length)
					appendData = null;	// appendData consumed
				
				appendDataOffset += bytesRead;
				ret += bytesRead;
			}

			return ret;
		}
	}
}
