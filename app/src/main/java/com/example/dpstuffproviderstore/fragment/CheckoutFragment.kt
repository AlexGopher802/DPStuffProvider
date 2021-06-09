package com.example.dpstuffproviderstore.fragment

import android.content.Context
import android.os.Bundle
import android.util.Log
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import com.example.dpstuffproviderstore.MainActivity
import com.example.dpstuffproviderstore.R
import com.example.dpstuffproviderstore.models.OrderData
import com.example.dpstuffproviderstore.models.ProductData
import com.example.dpstuffproviderstore.other.ClientApiService
import com.google.gson.Gson
import kotlinx.android.synthetic.main.fragment_checkout.*
import kotlinx.android.synthetic.main.fragment_checkout.view.*
import java.util.*

/**
 * Фрагмент оформления нового заказа
 */
class CheckoutFragment : Fragment() {

    var inflate : View? = null
    var checkSumm : Double = 0.0

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        inflate = inflater.inflate(R.layout.fragment_checkout, container, false)
        val mainActivity = activity as MainActivity

        //inflate!!.dateDelivery.minDate = java.util.Calendar.getInstance()
        inflate!!.tvCheckoutConfirm.text = "Итого: ${checkSumm} ₽"

        var c = Calendar.getInstance()
        c.add(Calendar.DAY_OF_MONTH, 1)
        inflate!!.dateDelivery.minDate = c.timeInMillis
        c.add(Calendar.DAY_OF_MONTH, 14)
        inflate!!.dateDelivery.maxDate = c.timeInMillis

        var frontDoor : Int? = null
        var apartNum : Int? = null
        var floorNum : Int? = null

        if(!inflate!!.inputTextFrontDoorCheckout.text.isNullOrEmpty()){
            frontDoor = inflate!!.inputTextFrontDoorCheckout.text.toString().toInt()
        }

        if(!inflate!!.inputTextApartCheckout.text.isNullOrEmpty()){
            apartNum = inflate!!.inputTextApartCheckout.text.toString().toInt()
        }

        if(!inflate!!.inputTextFloorCheckout.text.isNullOrEmpty()){
            floorNum = inflate!!.inputTextFloorCheckout.text.toString().toInt()
        }

        inflate!!.btnCheckoutConfirm.setOnClickListener {
            if(!validForm()){
                return@setOnClickListener
            }

            val newOrder = OrderData(
                    id = null,
                    address = inflate!!.inputTextAddressCheckout.text.toString(),
                    lastName = mainActivity.client!!.lastName,
                    firstName = mainActivity.client!!.firstName,
                    phone = mainActivity.client!!.phone,
                    frontDoor = frontDoor,
                    apartNum = apartNum,
                    floorNum = floorNum,
                    intercom = inflate!!.inputTextIntercomCheckout.text.toString(),
                    deliveryDate = "${inflate!!.dateDelivery.dayOfMonth}.${inflate!!.dateDelivery.month + 1}.${inflate!!.dateDelivery.year}",
                    timeFrom = inflate!!.spinnerTimeFrom.selectedItem.toString(),
                    timeTo = inflate!!.spinnerTimeTo.selectedItem.toString(),
                    commentary = inflate!!.inputTextCommentaryCheckout.text.toString(),
                    summ = checkSumm,
                    status = null,
                    idCourier = null,
                    priority = 5,
                    codeToFinish = ((1000..9999).random().toString()),
                    listProducts = mainActivity.cartList
            )

            Log.i("myLog", "checkout: ${Gson().toJson(newOrder.listProducts)}")

            ClientApiService().addOrder(newOrder){
                if(it == null){
                    Toast.makeText(requireContext(), "Ошибка, повторите позже", Toast.LENGTH_LONG).show()
                    mainActivity.makeCurrentFragment(CartFragment())
                }
                else{
                    Toast.makeText(requireContext(), "Заказ успешно оформлен", Toast.LENGTH_LONG).show()
                    mainActivity.cartList = arrayListOf()
                    mainActivity.cartFragment = CartEmptyFragment()
                    mainActivity.getSharedPreferences("sp", Context.MODE_PRIVATE).edit().putString("cartList", Gson().toJson(mainActivity.cartList)).apply()
                    mainActivity.makeCurrentFragment(CartEmptyFragment())
                }
            }
        }

        return inflate
    }

    fun validForm(): Boolean{
        if(inflate!!.inputTextAddressCheckout.text.isNullOrEmpty()){
            Toast.makeText(requireContext(), "Поле адреса обязательно для заполнения", Toast.LENGTH_LONG).show()
            return false
        }

        if(inflate!!.spinnerTimeFrom.selectedItemId >= inflate!!.spinnerTimeTo.selectedItemId){
            Toast.makeText(requireContext(), "Некорректное время доставки", Toast.LENGTH_LONG).show()
            return false
        }

        return true
    }
}