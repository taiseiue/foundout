particlesJS("particles-js",{
    "particles":{
      "number":{
        "value":128,//この数値を変更すると紙吹雪の数が増減できる
        "density":{
          "enable":false,
          "value_area":400
        }
      },
      "color": {
          "value": ["#EA5532", "#F6AD3C", "#FFF33F", "#00A95F", "#00ADA9", "#00AFEC","#4D4398", "#E85298"]//紙吹雪の色の数を増やすことが出来る
      },
      "shape":{
        "type":"polygon",//形状はpolygonを指定
        "stroke":{
          "width":0,
        },
        "polygon":{
          "nb_sides":5//多角形の角の数
        }
        },
        "opacity":{
          "value":1,
          "random":false,
          "anim":{
            "enable":true,
            "speed":20,
            "opacity_min":0,
            "sync":false
          }
        },
        "size":{
          "value":5.305992965476349,
          "random":true,//サイズをランダムに
          "anim":{
            "enable":true,
            "speed":1.345709068776642,
            "size_min":0.8,
            "sync":false
          }
        },
        "line_linked":{
          "enable":false,
        },
        "move":{
          "enable":true,
        "speed":8,//この数値を小さくするとゆっくりな動きになる
        "direction":"bottom",//下に向かって落ちる
        "random":false,//動きはランダムにならないように
        "straight":false,//動きをとどめない
        "out_mode":"out",//画面の外に出るように描写
        "bounce":false,//跳ね返りなし
          "attract":{
            "enable":false,
            "rotateX":600,
            "rotateY":1200
          }
        }
      },
      "interactivity":{
        "detect_on":"canvas",
        "events":{
          "onhover":{
            "enable":false,
          },
          "onclick":{
            "enable":false,
          },
          "resize":true
        },
      },
      "retina_detect":true
    });

/*
//カウントアップアニメーション
$('#box1').on('inview', function(event, isInView) {
    if (isInView) {
        //要素が見えたときに実行する処理
        $("#box1 .count-up").each(function(){
            $(this).prop('Counter',0).animate({//0からカウントアップ
                Counter: $(this).text()
            }, {
            // スピードやアニメーションの設定
                duration: 4000,//数字が大きいほど変化のスピードが遅くなる。2000=2秒
                easing: 'swing',//動きの種類。他にもlinearなど設定可能
                step: function (now) {
                    $(this).text(Math.ceil(now));
                }
            });
        });
    }
});
*/


// TextTypingというクラス名がついている子要素（span）を表示から非表示にする定義
function TextTypingAnime() {
    $('.TextTyping').each(function () {
      var elemPos = $(this).offset().top - 50;
      var scroll = $(window).scrollTop();
      var windowHeight = $(window).height();
      var thisChild = "";
      if (scroll >= elemPos - windowHeight) {
        thisChild = $(this).children(); //spanタグを取得
        //spanタグの要素の１つ１つ処理を追加
        thisChild.each(function (i) {
          var time = 100;
          //時差で表示する為にdelayを指定しその時間後にfadeInで表示させる
          $(this).delay(time * i).fadeIn(time);
        });
      } else {
        thisChild = $(this).children();
        thisChild.each(function () {
          $(this).stop(); //delay処理を止める
          $(this).css("display", "none"); //spanタグ非表示
        });
      }
    });
  }

// 画面が読み込まれたらすぐに動かしたい場合の記述
$(window).on('load', function () {
    //spanタグを追加する
    var element = $(".TextTyping");
    element.each(function () {
      var text = $(this).html();
      var textbox = "";
      text.split('').forEach(function (t) {
        if (t !== " ") {
          textbox += '<span>' + t + '</span>';
        } else {
          textbox += t;
        }
      });
      $(this).html(textbox);
  
    });
  
    TextTypingAnime();/* アニメーション用の関数を呼ぶ*/
  });// ここまで画面が読み込まれたらすぐに動かしたい場合の記述
