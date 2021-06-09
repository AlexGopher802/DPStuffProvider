package com.example.dpstuffprovider

import android.app.Dialog
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.Toast
import androidx.fragment.app.FragmentManager
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.dpstuffprovider.adapters.OrderComposAdapter
import com.example.dpstuffprovider.api.ApiService
import com.example.dpstuffprovider.fragments.DialogFinishOrderFragment
import com.example.dpstuffprovider.models.ClientData
import com.example.dpstuffprovider.models.CouriersData
import com.example.dpstuffprovider.models.OrderCompos
import com.example.dpstuffprovider.models.OrdersData
import kotlinx.android.synthetic.main.activity_more_info.*
import java.lang.Exception

/**
 * Активити с подробной информацией о выбранном заказе
 */
class MoreInfo : AppCompatActivity() {

    var orderInfo: OrdersData? = null
    var courier: CouriersData? = null
    var clientInfo: ClientData? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_more_info)

        try{
            orderInfo = intent.getSerializableExtra("orderInfo") as OrdersData?
            courier = intent.getSerializableExtra("courier") as CouriersData?
            tvAddress.text = orderInfo!!.address
            tvFrontDoor.text = "Подъезд: ${orderInfo!!.frontDoor.toString()}"
            tvFloorNum.text = "Этаж: ${orderInfo!!.floorNum.toString()}"
            tvAppartmentNum.text = "Квартира: ${orderInfo!!.apartNum.toString()}"
            tvIntercom.text = "Домофон: ${orderInfo!!.intercom}"
            clientInfo = intent.getSerializableExtra("clientInfo") as ClientData?
            tvClientName.text = "${clientInfo!!.firstName} ${clientInfo!!.patronymic}"
            tvClientPhone.text = "Телефон: ${clientInfo!!.phone}"

            btnCheckMap.setOnClickListener {
                Toast.makeText(applicationContext, "Скоро станет доступным...", Toast.LENGTH_LONG).show()
            }

            if(orderInfo!!.status == "Выдан курьеру"){
                btnEndOrder.visibility = View.VISIBLE
                btnConfirmOrder.visibility = View.GONE
            }
            else{
                btnEndOrder.visibility = View.GONE
                btnConfirmOrder.visibility = View.VISIBLE
            }

            btnConfirmOrder.setOnClickListener {
                orderInfo!!.idCourier = courier!!.id
                orderInfo!!.status = "Выдан курьеру"
                ApiService().updateOrderStatus(orderInfo!!){
                    if(it != null){
                        Toast.makeText(applicationContext, "Вы успешно взяли заказ", Toast.LENGTH_LONG).show()
                        finish()
                    }
                    else{
                        Toast.makeText(applicationContext, "Неизвестная ошибка, повторите позже", Toast.LENGTH_LONG).show()
                    }
                }
            }

            btnEndOrder.setOnClickListener {
                DialogFinishOrderFragment().show(supportFragmentManager, "dialog")
            }
        }
        catch (e: Exception){
            tvAddress.text = e.message.toString()
        }

        recyclerProducts.layoutManager = LinearLayoutManager(applicationContext)
        recyclerProducts.adapter =
            OrderComposAdapter(
                (intent.getSerializableExtra("orderCompos") as OrderCompos?)!!.listOrderCompos
            )
    }
}
