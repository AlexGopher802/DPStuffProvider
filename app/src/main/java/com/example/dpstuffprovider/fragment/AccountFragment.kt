package com.example.dpstuffprovider.fragment

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.FrameLayout
import com.example.dpstuffprovider.MainMenu
import com.example.dpstuffprovider.R
import kotlinx.android.synthetic.main.fragment_account.view.*

class AccountFragment : Fragment() {

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {

        val inflate : FrameLayout = inflater.inflate(R.layout.fragment_account, container, false) as FrameLayout

        val mainMenu = activity as MainMenu
        inflate.nameCourier.text = "${mainMenu.courier!!.lastName} ${mainMenu.courier!!.firstName} ${mainMenu.courier!!.patronymic}"

        return inflate
    }
}