import React, { Component } from "react";
import { ApplicationPaths } from "./api-authorization/ApiAuthorizationConstants";

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
      <div>
        <h1 style={{marginTop:"45px"}} className="text-center">سامانه CRM دستگاه های تعمیری براون موسوی</h1>
        <img className="mx-auto d-block" alt="crm-main-img" src={require('../content/images/crm-main.jpg')}></img>
        <p>این سایت سامانه مشتریان نمایندگی براون موسوی میباشد که میتوانید:</p>
        <ul>
          <li>پیگیری تعمیرات دستگاه های شما</li>
          <li>ثبت درخواست های جدید</li>
          <li>پرداخت به صورت آنلاین و ارسال درب منزل</li>
        </ul>
        <p>
          برای استفاده از خدمات CRM ما میتوانید از قسمت{" "}
          <a href={ApplicationPaths.Register}>ثبت نام</a> وارد پنل کاربری خود
          شوید.
        </p>
        <p>
          <b>
            کلیه حقوق این سایت متعلق به نمایندگی براون موسوی{" "}
            <a href="https://rishtarash.com/">
              (فروشگاه آنلاین ریش تراش ایران)
            </a>{" "}
            می‌باشد
          </b>
        </p>

        <div>
          تهران – خیابان حافظ – نرسیده به جمهوری – روبروی وزارت بهداشت – پلاک
          315 – نمایندگی موسوی تلفن تماس : 02166737691 شماره همراه : 09120710235
        </div>
      </div>
    );
  }
}
