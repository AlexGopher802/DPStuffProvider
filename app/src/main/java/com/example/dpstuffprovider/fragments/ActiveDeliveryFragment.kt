package com.example.dpstuffprovider.fragments

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.FrameLayout
import com.example.dpstuffprovider.R

/**
 * Фрагмент с активными заказами курьера
 */
class ActiveDeliveryFragment : Fragment() {

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {

        val inflate : FrameLayout = inflater.inflate(R.layout.fragment_active_delivery, container, false) as FrameLayout



        return inflate
    }
}