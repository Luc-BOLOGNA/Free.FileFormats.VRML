﻿using System.Collections.Generic;
using Free.FileFormats.VRML.Interfaces;

namespace Free.FileFormats.VRML.Nodes
{
	/// <summary>
	/// The PointSet node specifies a set of 3D points, in the local coordinate
	/// system, with associated colours at each point.
	/// </summary>
	public class x3dPointSet : X3DNode, X3DGeometryNode, IX3DPointPickSensorPickingGeometry
	{
		public List<X3DVertexAttributeNode> Attrib { get; set; }
		public X3DColorNode Color { get; set; }
		public X3DCoordinateNode Coord { get; set; }
		public IX3DFogCoordinateNode FogCoord { get; set; }

		public x3dPointSet()
		{
			Attrib=new List<X3DVertexAttributeNode>();
		}

		internal override X3DPrototypeInstance GetProto() { return new x3dPointSetPROTO(); }

		internal override bool ParseNodeBodyElement(string id, VRMLParser parser)
		{
			int line=parser.Line;

			if(id=="attrib")
			{
				List<X3DNode> nodes=parser.ParseSFNodeOrMFNodeValue();
				foreach(X3DNode node in nodes)
				{
					X3DVertexAttributeNode attr=node as X3DVertexAttributeNode;
					if(attr==null) parser.ErrorParsingNode(VRMLReaderError.UnexpectedNodeType, this, id, node, line);
					else Attrib.Add(attr);
				}
			}
			else if(id=="color")
			{
				X3DNode node=parser.ParseSFNodeValue();
				if(node!=null)
				{
					Color=node as X3DColorNode;
					if(Color==null) parser.ErrorParsingNode(VRMLReaderError.UnexpectedNodeType, this, id, node, line);
				}
			}
			else if(id=="coord")
			{
				X3DNode node=parser.ParseSFNodeValue();
				if(node!=null)
				{
					Coord=node as X3DCoordinateNode;
					if(Coord==null) parser.ErrorParsingNode(VRMLReaderError.UnexpectedNodeType, this, id, node, line);
				}
			}
			else if(id=="fogCoord")
			{
				X3DNode node=parser.ParseSFNodeValue();
				if(node!=null)
				{
					FogCoord=node as IX3DFogCoordinateNode;
					if(FogCoord==null) parser.ErrorParsingNode(VRMLReaderError.UnexpectedNodeType, this, id, node, line);
				}
			}
			else return false;
			return true;
		}
	}
}
