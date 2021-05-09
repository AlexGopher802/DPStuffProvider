package com.example.dpstuffprovider

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.dpstuffprovider.models.ClientData
import com.example.dpstuffprovider.models.OrderCompos
import com.example.dpstuffprovider.models.OrdersData
import kotlinx.android.synthetic.main.activity_more_info.*
import java.lang.Exception

class MoreInfo : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_more_info)

        try{
            val orderInfo = intent.getSerializableExtra("orderInfo") as OrdersData?
            tvAddress.text = orderInfo!!.address
            tvFrontDoor.text = "Подъезд: ${orderInfo.frontDoor.toString()}"
            tvFloorNum.text = "Этаж: ${orderInfo.floorNum.toString()}"
            tvAppartmentNum.text = "Квартира: ${orderInfo.apartNum.toString()}"
            tvIntercom.text = "Домофон: ${orderInfo.intercom}"
            val clientInfo = intent.getSerializableExtra("clientInfo") as ClientData?
            tvClientName.text = "${clientInfo!!.firstName} ${clientInfo.patronymic}"
            tvClientPhone.text = "Телефон: ${clientInfo.phone}"

        }
        catch (e: Exception){
            tvAddress.text = e.message.toString()
        }

        recyclerProducts.layoutManager = LinearLayoutManager(applicationContext)
        recyclerProducts.adapter = OrderComposAdapter((intent.getSerializableExtra("orderCompos") as OrderCompos?)!!.listOrderCompos)
    }
}
