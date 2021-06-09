package com.example.dpstuffprovider.fragments

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.FrameLayout
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.dpstuffprovider.MainMenu
import com.example.dpstuffprovider.R
import com.example.dpstuffprovider.adapters.OrdersAdapter
import com.example.dpstuffprovider.api.ApiService
import kotlinx.android.synthetic.main.fragment_active_delivery.*
import kotlinx.android.synthetic.main.fragment_active_delivery.view.*
import kotlinx.android.synthetic.main.fragment_all_delivery.view.*

/**
 * Фрагмент с активными заказами курьера
 */
class ActiveDeliveryFragment : Fragment() {

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val inflate = inflater.inflate(R.layout.fragment_active_delivery, container, false)
        val parentActivity = activity as MainMenu

        inflate.recyclerActiveOrders.layoutManager = LinearLayoutManager(context!!)

        inflate.progressBarActive.visibility = View.VISIBLE
        ApiService().getActiveOrdersByCourier(parentActivity.courier!!.id) {
            if(it != null){
                inflate.progressBarActive.visibility = View.GONE
                inflate.hintEmpty.visibility = View.GONE
                inflate.recyclerActiveOrders.adapter = OrdersAdapter(it, parentActivity)
            }
            else{
                inflate.progressBarActive.visibility = View.GONE
                inflate.hintEmpty.visibility = View.VISIBLE
            }
        }

        return inflate
    }
}