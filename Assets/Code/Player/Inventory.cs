using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;
namespace Player
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory instance;
        [SerializeField] Transform frontArm;
        [SerializeField] Transform gunPoint;
        [SerializeField] Animator backArmAnimator;
        [SerializeField] Transform backArmTarget;
        [SerializeField] GunClassHandler gunClassHandler;
        [Tooltip("Back arm LimbSolver2D to update the arm target following player aim with two hands gun.")]
        [SerializeField] LimbSolver2D limbSolver2D;
        private PlayerController playerController;
        public GameObject[] guns { get; set; }

        private void Awake()
        {
            playerController = GetComponent<PlayerController>();
            instance = this;
            gunClassHandler.GetClass(ClassHandler.classIndex);
        }
        private void OnEnable() {
            GunUISwapper.gunSwapper += GrabAmmo;
        }
        public void SetGun(int index)
        {
            GunUISwapper.gunSwapper.Invoke(index);
        }
        private void GrabAmmo(int index)
        {
            Transform otherTransform = guns[index].transform;
            Vector2 scale = otherTransform.localScale;
            otherTransform.SetParent(frontArm);
            otherTransform.position = gunPoint.position;
            otherTransform.localScale = gunPoint.localScale; // the scale sets back to x:1,y:1

            otherTransform.rotation = gunPoint.transform.parent.rotation;
            Gun gun = guns[index].GetComponent<Gun>();
            playerController.gun = gun;
            if (gun.GunGrabType == Gun.HandsForGrab.two)
            {
                limbSolver2D.gameObject.SetActive(true);
                Transform grabPoint = guns[index].GetChild(1).transform;
                playerController.twoHandsGun = grabPoint;
                backArmTarget.SetParent(grabPoint);
                backArmTarget.localPosition = Vector2.zero;
                backArmAnimator.enabled = false;
            }
        }
        private void OnDisable() {
            GunUISwapper.gunSwapper-=GrabAmmo;
        }
    }
}