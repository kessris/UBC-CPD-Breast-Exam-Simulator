using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0.15,0.15]")]
	public partial class DoctorNetworkControllerNetworkObject : NetworkObject
	{
		public const int IDENTITY = 3;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private Vector3 _RightHandPosition;
		public event FieldEvent<Vector3> RightHandPositionChanged;
		public InterpolateVector3 RightHandPositionInterpolation = new InterpolateVector3() { LerpT = 0.15f, Enabled = true };
		public Vector3 RightHandPosition
		{
			get { return _RightHandPosition; }
			set
			{
				// Don't do anything if the value is the same
				if (_RightHandPosition == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_RightHandPosition = value;
				hasDirtyFields = true;
			}
		}

		public void SetRightHandPositionDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_RightHandPosition(ulong timestep)
		{
			if (RightHandPositionChanged != null) RightHandPositionChanged(_RightHandPosition, timestep);
			if (fieldAltered != null) fieldAltered("RightHandPosition", _RightHandPosition, timestep);
		}
		[ForgeGeneratedField]
		private Vector3 _LeftHandPosition;
		public event FieldEvent<Vector3> LeftHandPositionChanged;
		public InterpolateVector3 LeftHandPositionInterpolation = new InterpolateVector3() { LerpT = 0.15f, Enabled = true };
		public Vector3 LeftHandPosition
		{
			get { return _LeftHandPosition; }
			set
			{
				// Don't do anything if the value is the same
				if (_LeftHandPosition == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x2;
				_LeftHandPosition = value;
				hasDirtyFields = true;
			}
		}

		public void SetLeftHandPositionDirty()
		{
			_dirtyFields[0] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_LeftHandPosition(ulong timestep)
		{
			if (LeftHandPositionChanged != null) LeftHandPositionChanged(_LeftHandPosition, timestep);
			if (fieldAltered != null) fieldAltered("LeftHandPosition", _LeftHandPosition, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			RightHandPositionInterpolation.current = RightHandPositionInterpolation.target;
			LeftHandPositionInterpolation.current = LeftHandPositionInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _RightHandPosition);
			UnityObjectMapper.Instance.MapBytes(data, _LeftHandPosition);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_RightHandPosition = UnityObjectMapper.Instance.Map<Vector3>(payload);
			RightHandPositionInterpolation.current = _RightHandPosition;
			RightHandPositionInterpolation.target = _RightHandPosition;
			RunChange_RightHandPosition(timestep);
			_LeftHandPosition = UnityObjectMapper.Instance.Map<Vector3>(payload);
			LeftHandPositionInterpolation.current = _LeftHandPosition;
			LeftHandPositionInterpolation.target = _LeftHandPosition;
			RunChange_LeftHandPosition(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _RightHandPosition);
			if ((0x2 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _LeftHandPosition);

			// Reset all the dirty fields
			for (int i = 0; i < _dirtyFields.Length; i++)
				_dirtyFields[i] = 0;

			return dirtyFieldsData;
		}

		protected override void ReadDirtyFields(BMSByte data, ulong timestep)
		{
			if (readDirtyFlags == null)
				Initialize();

			Buffer.BlockCopy(data.byteArr, data.StartIndex(), readDirtyFlags, 0, readDirtyFlags.Length);
			data.MoveStartIndex(readDirtyFlags.Length);

			if ((0x1 & readDirtyFlags[0]) != 0)
			{
				if (RightHandPositionInterpolation.Enabled)
				{
					RightHandPositionInterpolation.target = UnityObjectMapper.Instance.Map<Vector3>(data);
					RightHandPositionInterpolation.Timestep = timestep;
				}
				else
				{
					_RightHandPosition = UnityObjectMapper.Instance.Map<Vector3>(data);
					RunChange_RightHandPosition(timestep);
				}
			}
			if ((0x2 & readDirtyFlags[0]) != 0)
			{
				if (LeftHandPositionInterpolation.Enabled)
				{
					LeftHandPositionInterpolation.target = UnityObjectMapper.Instance.Map<Vector3>(data);
					LeftHandPositionInterpolation.Timestep = timestep;
				}
				else
				{
					_LeftHandPosition = UnityObjectMapper.Instance.Map<Vector3>(data);
					RunChange_LeftHandPosition(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (RightHandPositionInterpolation.Enabled && !RightHandPositionInterpolation.current.UnityNear(RightHandPositionInterpolation.target, 0.0015f))
			{
				_RightHandPosition = (Vector3)RightHandPositionInterpolation.Interpolate();
				//RunChange_RightHandPosition(RightHandPositionInterpolation.Timestep);
			}
			if (LeftHandPositionInterpolation.Enabled && !LeftHandPositionInterpolation.current.UnityNear(LeftHandPositionInterpolation.target, 0.0015f))
			{
				_LeftHandPosition = (Vector3)LeftHandPositionInterpolation.Interpolate();
				//RunChange_LeftHandPosition(LeftHandPositionInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public DoctorNetworkControllerNetworkObject() : base() { Initialize(); }
		public DoctorNetworkControllerNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public DoctorNetworkControllerNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
