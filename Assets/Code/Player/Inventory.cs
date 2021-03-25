using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D.IK;
namespace Game.Player
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
        public static GameObject[] guns;
        public int gunIndex{get;set;}=0;
        private void Awake()
        {
            playerController = GetComponent<PlayerController>();
            instance = this;
            guns=new GameObject[2];
            for(int i=0;i<guns.Length;i++){
                guns[i]=Instantiate(gunClassHandler.GetClass(ClassHandler.classIndex)[i], gunPoint, frontArm);
                SetGunFirstTime(i);
            }
        }
        private void SetGunFirstTime(int index){
            var obj = guns[index];
            Transform gunTransform = guns[index].transform;
            gunTransform.SetParent(frontArm);
            gunTransform.position = gunPoint.position;
            gunTransform.localScale = gunPoint.localScale; // the scale sets back to x:1,y:1

            gunTransform.rotation = gunPoint.transform.parent.rotation;
            Gun gun = guns[index].GetComponent<Gun>();
            GunUIHandler.gunInterfaceSetter.Invoke(gun);
            if (index != 0) obj.SetActive(false);
            else playerController.gun = gun;
        }
        public void GrabAmmo(bool active)
        {
            Gun gun = guns[gunIndex].GetComponent<Gun>();
            gun.gameObject.SetActive(active);
            playerController.gun = gun;
            if (gun.GunGrabType == Gun.HandsForGrab.two)
            {
                limbSolver2D.gameObject.SetActive(active);
                Transform grabPoint = guns[gunIndex].GetChild(1).transform;
                playerController.twoHandsGun = grabPoint;
                backArmTarget.SetParent(grabPoint);
                backArmTarget.localPosition = Vector2.zero;
                backArmAnimator.enabled = !active;
            }
        }
    }
}