using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private PlayerEx player;
    public List<Slotdata> slots = new List<Slotdata>();
    public int maxSlot = 4;
    public int currentItemSlot = 0;
    private void Awake()
    {
        player = GetComponent<PlayerEx>();
        for (int i = 0; i < maxSlot; i++)
        {
            slots.Add(new Slotdata());
        }
    }
    public virtual void AddtoInventory(GameObject item)
    {
        //���� ���Կ� �������� �ִ���?
        //�������� ��´�
        //������ ���Կ� �߰��Ѵ�
        if (slots[currentItemSlot].isEmpty)
        {
            slots[currentItemSlot].isEmpty = false;
            slots[currentItemSlot].slotObj = item;
            item.GetComponent<Item>().PickUp(player);
        }
    }
    //���� ����ִ� ������ ������ �Լ�
    public void RemovetoInventory()
    {
        if (slots[currentItemSlot].isEmpty == false)
        {
            slots[currentItemSlot].slotObj.GetComponent<Item>().PickDown(player);
            slots[currentItemSlot].isEmpty = true;
            slots[currentItemSlot].slotObj = null;
        }
    }
    public virtual void ChangeItemSlot(int index)
    {
        if (index < 0 || index > 3)
            return;
        GetCurrentItem()?.SetActive(false);
        currentItemSlot = index;
        GetCurrentItem()?.SetActive(true);
    }

    public virtual GameObject GetCurrentItem() => slots[currentItemSlot].slotObj;
    
}
