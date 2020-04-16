//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.12
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace Autodesk.Fbx {

public class FbxSkin : FbxDeformer {
  internal FbxSkin(global::System.IntPtr cPtr, bool ignored) : base(cPtr, ignored) { }

  // override void Dispose() {base.Dispose();}

  public new static FbxSkin Create(FbxManager pManager, string pName) {
    global::System.IntPtr cPtr = NativeMethods.FbxSkin_Create__SWIG_0(FbxManager.getCPtr(pManager), pName);
    FbxSkin ret = (cPtr == global::System.IntPtr.Zero) ? null : new FbxSkin(cPtr, false);
    if (NativeMethods.SWIGPendingException.Pending) throw NativeMethods.SWIGPendingException.Retrieve();
    return ret;
  }

  public new static FbxSkin Create(FbxObject pContainer, string pName) {
    global::System.IntPtr cPtr = NativeMethods.FbxSkin_Create__SWIG_1(FbxObject.getCPtr(pContainer), pName);
    FbxSkin ret = (cPtr == global::System.IntPtr.Zero) ? null : new FbxSkin(cPtr, false);
    if (NativeMethods.SWIGPendingException.Pending) throw NativeMethods.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool AddCluster(FbxCluster pCluster) {
    bool ret = NativeMethods.FbxSkin_AddCluster(swigCPtr, FbxCluster.getCPtr(pCluster));
    if (NativeMethods.SWIGPendingException.Pending) throw NativeMethods.SWIGPendingException.Retrieve();
    return ret;
  }

  public FbxCluster GetCluster(int pIndex) {
    global::System.IntPtr cPtr = NativeMethods.FbxSkin_GetCluster(swigCPtr, pIndex);
    FbxCluster ret = (cPtr == global::System.IntPtr.Zero) ? null : new FbxCluster(cPtr, false);
    if (NativeMethods.SWIGPendingException.Pending) throw NativeMethods.SWIGPendingException.Retrieve();
    return ret;
  }

  public override int GetHashCode(){
      return swigCPtr.Handle.GetHashCode();
  }

  public bool Equals(FbxSkin other) {
    if (object.ReferenceEquals(other, null)) { return false; }
    return this.swigCPtr.Handle.Equals (other.swigCPtr.Handle);
  }

  public override bool Equals(object obj){
    if (object.ReferenceEquals(obj, null)) { return false; }
    /* is obj a subclass of this type; if so use our Equals */
    var typed = obj as FbxSkin;
    if (!object.ReferenceEquals(typed, null)) {
      return this.Equals(typed);
    }
    /* are we a subclass of the other type; if so use their Equals */
    if (typeof(FbxSkin).IsSubclassOf(obj.GetType())) {
      return obj.Equals(this);
    }
    /* types are unrelated; can't be a match */
    return false;
  }

  public static bool operator == (FbxSkin a, FbxSkin b) {
    if (object.ReferenceEquals(a, b)) { return true; }
    if (object.ReferenceEquals(a, null) || object.ReferenceEquals(b, null)) { return false; }
    return a.Equals(b);
  }

  public static bool operator != (FbxSkin a, FbxSkin b) {
    return !(a == b);
  }

}

}
