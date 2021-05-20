package com.example.dpstuffproviderstore.fragment

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import com.example.dpstuffproviderstore.MainActivity
import com.example.dpstuffproviderstore.R
import kotlinx.android.synthetic.main.activity_main.*
import kotlinx.android.synthetic.main.fragment_cart.view.*

class CartEmptyFragment : Fragment() {

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val inflate = inflater.inflate(R.layout.fragment_cart_empty, container, false)

        val mainActivity = activity as MainActivity

        inflate.btnGoToCatalog.setOnClickListener {
            mainActivity.bottomNavMenu.selectedItemId = R.id.catalog
        }

        return inflate
    }

}