//---------------------------------------------------------------------
// File: PartyResolutionStream.cs
// 
// Sample: CustomPartyResolution
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



using System;
using System.IO;
using System.Threading;
using Microsoft.BizTalk.Message.Interop;

namespace Microsoft.Samples.BizTalk.Pipelines.CustomPartyResolution_Pipeline_Component.CustomPartyResolution_Pipeline_Component
{
	/// <summary>
	/// Custom implementation of the base Stream class.  Overrides Stream.Read
	/// and fires callbacks when the required context propeties have been populated.
	/// Most implementation is simply default implementation of a Stream interface.
	/// Only custom implementation is in Stream.Read()
	/// </summary>
	public class PartyResolutionStream : Stream
	{
		Stream mBaseStream;
		FirstReadOccurred mFirstReadCallback = null;
		ReadOccurred mReadCallback = null;
		LastReadOccurred mLastReadCallback = null;
		bool mFirstRead = true;
		// WaitHandle mWaitHandle = null;

		/// <summary>
		/// Custom class constructor
		/// </summary>
		/// <param name="BaseStream">The underlying stream that the custom stream is wrapping</param>
		/// <param name="FirstReadCallback">Callback function to be called the first time that Read() is called.  This callback function
		///									will be called only once.</param>
		/// <param name="ReadCallback">The callback function to be called each time that Read() is called.  This callback function may be
		///									called multiple times</param>
		/// <param name="LastReadCallback">The callback function to be called the last time that Read() is called (when Read() returns 0 bytes).
		///									This callback function will be called only once.</param>
		public PartyResolutionStream(Stream BaseStream, 
									 FirstReadOccurred FirstReadCallback, 
									 ReadOccurred ReadCallback, 
									 LastReadOccurred LastReadCallback)
		{
			mBaseStream = BaseStream;
			mFirstReadCallback = FirstReadCallback;
			mReadCallback = ReadCallback;
			mLastReadCallback = LastReadCallback;
		}

		public delegate void FirstReadOccurred();
		public delegate void ReadOccurred();
		public delegate void LastReadOccurred();

		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return mBaseStream.BeginRead(buffer, offset, count, callback, state);
		}

		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return mBaseStream.BeginWrite(buffer, offset, count, callback, state);
		}

		public override bool CanRead
		{
			get
			{
				return mBaseStream.CanRead;
			}
		}

		public override bool CanSeek
		{
			get
			{
				return mBaseStream.CanSeek;
			}
		}

		public override bool CanWrite
		{
			get
			{
				return mBaseStream.CanWrite;
			}
		}

		public override void Close()
		{
			mBaseStream.Close();
		}

		public override System.Runtime.Remoting.ObjRef CreateObjRef(Type requestedType)
		{
			return mBaseStream.CreateObjRef(requestedType);
		}

		protected override System.Threading.WaitHandle CreateWaitHandle()
		{
			return new ManualResetEvent(false);
		}

		public override int EndRead(IAsyncResult asyncResult)
		{
			return mBaseStream.EndRead(asyncResult);
		}

		public override void EndWrite(IAsyncResult asyncResult)
		{
			mBaseStream.EndWrite(asyncResult);
		}

		public override bool Equals(object obj)
		{
			return mBaseStream.Equals(obj);
		}

		public override void Flush()
		{
			mBaseStream.Flush();
		}
		
		public override int GetHashCode()
		{
			return mBaseStream.GetHashCode();
		}

		public override object InitializeLifetimeService()
		{
			return mBaseStream.InitializeLifetimeService();
		}

		public override long Length
		{
			get
			{
				return mBaseStream.Length;
			}
		}

		public override long Position
		{
			get
			{
				return mBaseStream.Position;
			}
			set
			{
				mBaseStream.Position = value;
			}
		}
		/// <summary>
		/// Only custom implmentation in this overriden stream count
		/// This overridden implementation opitonally will invoke callback functions specified in the class constructor 
		/// for the following events:
		/// FirstRead:  occurs once the first time that Read() is called
		/// Read:  Occurs each time the Read() method is called
		/// LastRead:  Occurs once when Read() returns 0 for bytes read.  This indicates that the end of the stream has been reached.
		/// </summary>
		/// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between offset and (offset + count- 1) replaced by the bytes read from the current source. </param>
		/// <param name="offset">The zero-based byte offset in buffer at which to begin storing the data read from the current stream. </param>
		/// <param name="count">The maximum number of bytes to be read from the current stream.</param>
		/// <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.</returns>
		public override int Read(byte[] buffer, int offset, int count)
		{
			int ReturnValue = mBaseStream.Read(buffer, offset, count);

			if(mFirstRead)
			{
				mFirstRead = false;
				if(mFirstReadCallback != null)
					mFirstReadCallback();
			}
			if (ReturnValue != 0)
			{
				if (mReadCallback != null)
					mReadCallback();
			}
			else
			{
				if (mLastReadCallback != null)
					mLastReadCallback();
			}

			return ReturnValue;
		}

		public override int ReadByte()
		{
			return mBaseStream.ReadByte ();
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return mBaseStream.Seek(offset, origin);
		}

		public override void SetLength(long value)
		{
			mBaseStream.SetLength(value);
		}

		public override string ToString()
		{
			return mBaseStream.ToString ();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			mBaseStream.Write(buffer, offset, count);
		}

		public override void WriteByte(byte value)
		{
			mBaseStream.WriteByte (value);
		}

	}
}
