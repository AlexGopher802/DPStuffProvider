package com.example.dpstuffprovider.fragments

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.FrameLayout
import android.widget.Toast
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.dpstuffprovider.MainMenu
import com.example.dpstuffprovider.interfaces.IOrders
import com.example.dpstuffprovider.adapters.OrdersAdapter
import com.example.dpstuffprovider.R
import com.example.dpstuffprovider.api.ApiService
import com.example.dpstuffprovider.models.OrdersData
import kotlinx.android.synthetic.main.fragment_all_delivery.view.*
import kotlinx.coroutines.*
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

/**
 * Фрагмент со списком доступных заказов
 */
class AllDeliveryFragment : Fragment() {

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {

        val inflate : FrameLayout = inflater.inflate(R.layout.fragment_all_delivery, container, false) as FrameLayout

        inflate.recyclerOrders.layoutManager = LinearLayoutManager(context!!)

        ApiService().getOrdersByStatus("Обрабатывается") {
            if(it != null){
                inflate.recyclerOrders.adapter = OrdersAdapter(it, (activity as MainMenu))
            }
            else{
                //Обработка ошибки
            }
        }

        return inflate;
    }
}