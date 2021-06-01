package com.example.dpstuffproviderstore.fragment

import android.content.Context
import android.opengl.Visibility
import android.os.Bundle
import android.util.Log
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.dpstuffproviderstore.MainActivity
import com.example.dpstuffproviderstore.R
import com.example.dpstuffproviderstore.adapter.CartAdapter
import com.example.dpstuffproviderstore.adapter.OrderAdapter
import com.example.dpstuffproviderstore.other.ClientApiService
import com.example.dpstuffproviderstore.other.ServiceBuilder
import kotlinx.android.synthetic.main.fragment_cart.view.*
import kotlinx.android.synthetic.main.fragment_orders.view.*

class OrdersFragment : Fragment() {

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?,
                              savedInstanceState: Bundle?): View? {
        val inflate = inflater.inflate(R.layout.fragment_orders, container, false)
        val mainActivity = activity as MainActivity

        inflate!!.recyclerOrders.layoutManager = LinearLayoutManager(context!!)
        //inflate!!.recyclerCart.adapter = CartAdapter(mainActivity.cartList, this@CartFragment)
        Log.i("myLog", "${mainActivity.client!!.login!!}, ${mainActivity.getSharedPreferences("sp", Context.MODE_PRIVATE).getString("password", "")!!}")
        ClientApiService().getOrderByClient(mainActivity.client!!.login!!, mainActivity.getSharedPreferences("sp", Context.MODE_PRIVATE).getString("password", "")!!){
            if(it.isNullOrEmpty()){
                inflate!!.hintEmpty.visibility = View.VISIBLE
            }
            else{
                inflate!!.recyclerOrders.adapter = OrderAdapter(it, this@OrdersFragment)
            }
        }

        return inflate
    }

}