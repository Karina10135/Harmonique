using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KAM3RA
{
    public class PlayerController: MonoBehaviour
    {

        // true if attached to User
        public bool player = false;
        public PlayerController target;



        // collide radius
        public float radius = 0.3f;

        // per-frame velocity 
        protected Vector3 velocity = Vector3.zero;

        // current non-scaled input speed
        protected float speed = 0f;

        // whether or not to look at the target when there is one
        protected bool watchTarget = false;

        [HideInInspector]
        public bool moving;
        CameraMoveToPoint camChange;


        // maximum angle we collide at and still keep moving 
        // NOTE this is unsigned -- use with altitudeScale (below) 
        // to know whether you're on an upward or downward slope
        public float maxSlope = 80f;

        // sets rigidbody.drag when not 1) Idling (velocity == 0, drag = idleDrag) or 2) In-air (drag = 0)
        public float drag = 10f;

        // how high the actor can jump 
        public float jumpHeight = 2f;

        // where the camera is looking through the actor, 0-1 is from feet (presumed to be the actor's local position) to top of the head
        public float eyeHeightScale = 0.9f;

        // maximum speed of the actor we're controlling
        public float maxSpeed = 4f;

        // accleration - ramp up to speed, expressed as a percentage (0-1) of maxSpeed
        public float acceleration = 1f;

        // momentum -- dead-stop is zero 
        public float momentum = 0f;

        new public Rigidbody rigidbody;
        new public CapsuleCollider collider;
        new public SkinnedMeshRenderer renderer;
        public Animator anim;

        bool walking;


        // list of all Renderers that are children of this gameObject -- we need all Renderers to toggle visiblity on/off
        protected List<Renderer> renderers = new List<Renderer>();

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            collider = GetComponent<CapsuleCollider>();
            renderer = GetComponentInChildren<SkinnedMeshRenderer>();
            anim = GetComponent<Animator>();
            target = this;
        }

        public void ProcessAnimation()
        {
            anim.SetBool("Walk", walking);
        }

        public virtual void UserUpdate(CameraManager user)
        {


            // these values are 0 or 1 -- not variable
            Vector3 userVelocity = user.velocity;

            // range varies based on User sensitivity and damping
            Vector3 userAngular = user.angularVelocity;

            // rotate this transform to keep the actor facing forward
            transform.Rotate(userAngular, Space.World);

            bool walk = Mathf.Abs(userVelocity.z) == user.walkScale;
            if (userVelocity.z > 0f) walking = true; //State = walk ? "WalkForward" : "RunForward";
            else if (userVelocity.z < 0f) walking = false; //State = walk ? "WalkBack" : "RunBack";
            else if (userVelocity.x > 0f) walking = true;
            else if (userVelocity.x < 0f) walking = true;
            else
            {
                // not moving, so might be turning
                if (Mathf.Abs(userAngular.y) <= 1f) walking = false;
                else if (userAngular.y > 1f) walking = false;
                else if (userAngular.y < 1f) walking = false;
            }
            // we're damping velocity in Update() so we only set non-zero velocity here
            if (userVelocity != Vector3.zero) velocity = transform.TransformDirection(userVelocity) * ScaledSpeed;

            #region grayed

            //if (Colliding)
            //{
            //    // always use gravity when not in air
            //    //rigidbody.useGravity = true;
            //    // if flying, no jumping at all 
            //    //if (type == Type.Fly)
            //    //{
            //    //    if (speed == maxSpeed)
            //    //    {
            //    //        //State = "Fly";
            //    //        userVelocity.y = 1f;
            //    //    }
            //    //}

            //    //otherwise check to see if we're moving normally or are "walking"
            //    bool walk = Mathf.Abs(userVelocity.z) == user.walkScale;
            //    if (userVelocity.z > 0f) walking = true; //State = walk ? "WalkForward" : "RunForward";
            //    else if (userVelocity.z < 0f) State = walk ? "WalkBack" : "RunBack";
            //    else if (userVelocity.x > 0f) State = "StrafeRight";
            //    else if (userVelocity.x < 0f) State = "StafeLeft";
            //    else
            //    {
            //        // not moving, so might be turning
            //        if (Mathf.Abs(userAngular.y) <= 1f) State = "Idle";
            //        else if (userAngular.y > 1f) State = "TurnRight";
            //        else if (userAngular.y < 1f) State = "TurnLeft";
            //    }
            //    // we're damping velocity in Update() so we only set non-zero velocity here
            //    if (userVelocity != Vector3.zero) velocity = transform.TransformDirection(userVelocity) * ScaledSpeed;
            //}
            //else
            //{
            //    // no side-to-side movement whether flying or hovering, if not colliding
            //    userVelocity.x = 0f;
            //    // if we're not already flying
            //    //if (State != "Fly")
            //    //{
            //    //	// we have a timer here so we can still jump
            //    //	airTime = Mathf.Min(airTime + Time.deltaTime, canFlyAtTime);
            //    //	// if user is holding down the "jump" key and we've passed a little time
            //    //	if (userVelocity.y != 0 && airTime == canFlyAtTime) State = "Fly";
            //    //}
            //    //else
            //    //{
            //    //	// no gravity if hovering
            //    //	rigidbody.useGravity = (type == Type.Fly);					
            //    //	// if moving forward, so set to the current direction, otherwise, zero it out
            //    //	velocity  		= userVelocity.z > 0 ? user.Direction * MaxScaledSpeed : Vector3.zero;
            //    //	// but if we're still on the "jump" button, apply
            //    //	if (type == Type.Hover) velocity.y += userVelocity.y * MaxScaledSpeed * 2f;
            //    //	else  					velocity.y += userVelocity.y * MaxScaledSpeed * 0.5f;
            //    //}
            //}
            #endregion

            //if (userVelocity.x > 0f || userVelocity.x < 0f)
            //{
            //    ProcessAnimation(true);
            //}
            //else
            //{
            //    ProcessAnimation(false);
            //}

            //if (userVelocity.z > 0f || userVelocity.z < 0f)
            //{
            //    ProcessAnimation(true);

            //}
            //else
            //{
            //    ProcessAnimation(false);
            //}


        }


        protected virtual void Update()
        {
            // if not "in the air", adjust speed per accleration 

            float accel = acceleration * maxSpeed;
            speed = accel >= maxSpeed ? maxSpeed : (Mathf.Clamp(speed + Time.deltaTime * 4f, 0f, maxSpeed));
            
            // back off on velocity every frame -- only if gravity
            if (walking)
            {
                if (rigidbody.useGravity)
                {
                    velocity *= momentum;
                }
            }
            // if we have a target, stay put, and watch it if requested
            if (!player && target != null)
            {
                velocity = Vector3.zero;
                walking = false;
                //State = "Idle";
                if (watchTarget) User.LookAt2D(transform, target.transform.position);
            }
            
        }

        protected virtual void FixedUpdate()
        {
            // if idle, apply more drag to keep the actor from slipping
            // if Blocked or not colliding, cancel drag, we need to fall at maximum rate
            //rigidbody.drag = collisionState == CollisionState.Grounded ? drag : 0f;

            // if we're currently on the ground
            //if (Colliding)
            //{
            //    // if we're either Blocked or not colliding -- if Blocked we're stuck at maxSlope
            //    if (collisionState == CollisionState.Blocked)
            //    {
            //        velocity += maxCollisionPoint.normal;
            //        velocity.y = -collider.height;
            //    }

            //    if (velocity == Vector3.zero) rigidbody.drag = idleDrag;

            //    // add the velocity we set in Velocity, subtract the current rigidbody velocity since we're adding an incremental change
            //    rigidbody.AddForce(velocity - rigidbody.velocity, ForceMode.VelocityChange);

            //    // if jumping, set the current velocity's y-component directly
            //    if (collisionState == CollisionState.Grounded)
            //    {
            //        if (State != "Jump" && State != "Fly" && TooFast)
            //        {
            //            if (momentum == 0)
            //            {
            //                rigidbody.drag = idleDrag;
            //                rigidbody.velocity = -rigidbody.velocity.normalized * ScaledSpeed;
            //            }
            //        }
            //    }

            //    // reset fall velocity and relative altitude trackers
            //    fallVelocity = 0f;
            //    relativeAltitude = 0f;
            //}
            //else
            //{
            //    // track relative altitude
            //    relativeAltitude = transform.position.y;
            //    if (State == "Fly")
            //    {
            //        // is using gravity, only set velocity if it's not equal to zero
            //        if (rigidbody.useGravity)
            //        {
            //            if (velocity != Vector3.zero) rigidbody.velocity = velocity;
            //        }
            //        // otherwise set the velocity -- if it's zero, the actor will just sit in the air
            //        else
            //        {
            //            rigidbody.velocity = velocity;
            //        }
            //    }
            //    else
            //    {
            //        // if we're not in a fall state
            //        if (State != "Fall")
            //        {
            //            // we're falling at any velocity no matter how small 
            //            if (rigidbody.velocity.y < 0f)
            //            {
            //                // add them until we get to a reasonable cutoff
            //                fallVelocity += rigidbody.velocity.y;
            //                // now we're really falling!
            //                if (fallVelocity < fallVelocityCutoff)
            //                {
            //                    State = "Fall";
            //                }
            //            }
            //        }
            //    }

                
            //}
            // if moving, set an altitude state -- Level, Up or Down
            //if (States.Moving)
            //{
            //    altitudeState = (Math.Abs(rigidbody.velocity.y) < 1f ? AltitudeState.Level : (rigidbody.velocity.y > 0 ? AltitudeState.Up : AltitudeState.Down));
            //}
        }


        // reset rigidbody, rotation and default to Idle
        public virtual void Reset()
        {
            //State = "Idle";
            speed = 0f;
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
            if (rigidbody != null)
            {
                if (!rigidbody.isKinematic) rigidbody.velocity = rigidbody.angularVelocity = Vector3.zero;
                velocity = Vector3.zero;
                rigidbody.isKinematic = true;
                rigidbody.isKinematic = false;
            }
            SetEnabled(true);
        }

        public virtual void SetEnabled(bool enabled)
        {
            foreach (Renderer m in renderers) m.enabled = enabled;
        }
        protected virtual bool InitRenderers()
        {
            renderers = new List<Renderer>(GetComponentsInChildren<Renderer>(true));
            if (renderers.Count > 0)
            {
                int maxBones = 0; // we're looking for the SkinnedMeshRenderer with the most bones
                foreach (Renderer m in renderers)
                {
                    if (m is SkinnedMeshRenderer)
                    {
                        int b = ((SkinnedMeshRenderer)m).bones.Length;
                        if (b > maxBones) { maxBones = b; renderer = (SkinnedMeshRenderer)m; }
                    }
                }
                // no SkinnedMeshRenderers, look for the largest renderer
                if (renderer == null)
                {
                    float h = 0;
                    foreach (Renderer m in renderers)
                    {
                        if (m.bounds.size.y > h)
                        {
                            h = m.bounds.size.y;
                            renderer = (SkinnedMeshRenderer)m;
                        }
                    }
                }
                // this should never happen
                if (renderer == null)
                {
                    renderer = (SkinnedMeshRenderer)renderers[0];
                }
            }
            return (renderer != null);
        }

        public virtual void SetTarget(PlayerController target)
        {
            SetTarget(target, false);
        }
        public virtual void SetTarget(PlayerController target, bool watch)
        {
            this.target = target;
            watchTarget = watch;
        }




        //////////////////////////////////////////////////////////////
        // Other Properties 
        //////////////////////////////////////////////////////////////
        // current bounding radius based on renderer -- keep in mind this could change depending on animation
        // also NOTE that ParticleSystems and TextMesh are rightly not included in the computation
        public virtual float RendererBoundingRadius
        {
            get
            {
                Bounds bounds = new Bounds(transform.position, Vector3.zero);
                foreach (Renderer m in renderers)
                {
                    if (m.gameObject.GetComponent<ParticleSystem>() != null) continue;
                    if (m.gameObject.GetComponent<TextMesh>() != null) continue;
                    bounds.Encapsulate(m.bounds);
                }
                return bounds.extents.magnitude;
            }
        }
        // bounding radius based on colliders
        public virtual float BoundingRadius
        {
            get
            {
                List<Collider> colliders = new List<Collider>(GetComponentsInChildren<Collider>(true));
                Bounds bounds = new Bounds(transform.position, Vector3.zero);
                foreach (Collider m in colliders) bounds.Encapsulate(m.bounds);
                return bounds.extents.magnitude;
            }
        }

        // reasonable mass
        //public virtual float Mass { get { return User.CalculateMass(collider.bounds.extents.magnitude); } }
        // moving a bit faster than desired speed
        public virtual bool TooFast { get { return Speed > ScaledSpeed; } }
        // ... or a bit slower
        public virtual bool TooSlow { get { return Speed < ScaledSpeed; } }
        // actual current speed, *not* desired speed
        public virtual float Speed { get { return rigidbody.velocity.magnitude; } }
        // current horizontal speed, *not* desired speed
        public virtual float HorizontalSpeed { get { return User.SetVectorY(rigidbody.velocity, 0f).magnitude; } }
        // current speed adjusted by scale height of the object
        public virtual float ScaledSpeed { get { return speed * Height; } }
        // max speed adjusted by scale height of the object
        public virtual float MaxScaledSpeed { get { return maxSpeed * Height; } }
        // current position + adjustment for where the "eyes" are
        public virtual Vector3 EyePosition { get { return User.AddVectorY(transform.position, Height * eyeHeightScale); } } // NEED ******
        // same as above but the middle of the object
        public virtual Vector3 WaistPosition { get { return User.AddVectorY(transform.position, Height * 0.5f); } } // NEED ******
        // same as above but the bottom of the object
        public virtual Vector3 FootPosition { get { return User.AddVectorY(transform.position, Height * 0.1f); } } // NEED ******
        // simple collider radius
        public virtual float Radius { get { return radius; } } // NEED ****
        // we could use renderer.bounds.size.y * eyeHeightScale here, but that changes per frame a bit
        public virtual float Height { get { return collider.height * transform.localScale.y; } } // NEED ****
        // jump adjusted for scale and gravity
        public virtual float JumpVelocity { get { return Mathf.Sqrt(jumpHeight * Height * -Physics.gravity.y * 2f); } }
    }

}


