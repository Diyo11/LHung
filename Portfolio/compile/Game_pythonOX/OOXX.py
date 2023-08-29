# -*- coding: utf-8 -*-

#import pygame  #呼叫
#
#from pygame.locals import *  
#pygame.init()  #初始化
#
#screen = pygame.display.set_mode((500,365), 0, 32)
#pygame.display.set_caption("Hello, World!")
#
####沒回應

import pygame as pg

class Game :
    def __init__(self) :
        self.board = [
            [-1, -1, -1],
            [-1, -1, -1],
            [-1, -1, -1]
            ]
        self.x_point = [200, 400, 600]
        self.y_point = [160, 320, 480]
        self.start = False
#        self.miss = False              
        self.which_choice = False
        self.win = False
        self.choice_player = ''
        self.turn = 1
        self.x_pt = -1
        self.y_pt = -1
        self.screen = pg.display.set_mode((600, 480), 0, 32)            #設定視窗
        self.circle = pg.image.load('image/O.png').convert_alpha()
        self.x_photo = pg.image.load('image/X.png').convert_alpha()

    def interface_gui(self) :
        pg.display.set_caption('OOXX')                                  #設定視窗設定視窗名稱
        background = pg.image.load('image/first.png').convert()               #載入圖片
        self.screen.blit(background, (0, 0))
        pg.display.update()                                             #更新畫面
        self.event()

#    def miss(self) :
#        choice = pg.image.load('miss.png').convert()
#        self.screen.blit(choice, (0, 0))
#        pg.display.update()

    def game_start(self) :
        choice = pg.image.load('image/whostart.png').convert()
        self.screen.blit(choice, (0, 0))
        pg.display.update()

    def who_start(self, choice) :
        self.which_choice = True
        if choice == 'o' :
            self.choice_player = 'o'
        else :
            self.choice_player = 'x'
        start_game = pg.image.load('image/start_game.png').convert()
        self.screen.blit(start_game, (0, 0))
        pg.display.update()

    def getpos(self) :
        x, y = pg.mouse.get_pos()                                       #獲取鼠標位置 x y
        xp = False
        yp = False
        for x_pt in self.x_point :
            if x < x_pt and not xp:
                x = x_pt - 200
                xp = True

        for y_pt in self.y_point :
            if y < y_pt and not yp :
                y = y_pt - 160
                yp = True

        return x, y

    def search_board(self) :
        if self.board[self.y_pt][self.x_pt] == -1 :
            return True

    def set_xy(self, x, y) :
        for y_pt in range(3) :
            for x_pt in range(3) :
                if y == self.y_point[y_pt] - 160 and x == self.x_point[x_pt] - 200 :
                    self.y_pt, self.x_pt = y_pt, x_pt

    def set_board(self, choice) :
        if choice == 'o' :
            self.board[self.y_pt][self.x_pt] = 0
        else :
            self.board[self.y_pt][self.x_pt] = 1 

    def who_win(self, winner) :
        if winner == 0 :
            circle_win = pg.image.load('image/Owin.png').convert()
            self.screen.blit(circle_win, (0, 0))
            print('O')
        else :
            x_win = pg.image.load('image/Xwin.png').convert()
            self.screen.blit(x_win, (0, 0))

        pg.time.delay(1000)
        pg.display.update()

    def is_win(self) :
        wins = [
            [self.board[0][0], self.board[0][1], self.board[0][2]],
            [self.board[1][0], self.board[1][1], self.board[1][2]],
            [self.board[2][0], self.board[2][1], self.board[2][2]],
            [self.board[0][0], self.board[1][0], self.board[2][0]],
            [self.board[0][1], self.board[1][1], self.board[2][1]],
            [self.board[0][2], self.board[1][2], self.board[2][2]],
            [self.board[0][0], self.board[1][1], self.board[2][2]],
            [self.board[0][2], self.board[1][1], self.board[2][0]]
        ]

        for win in wins :
            if not min(win) == -1 and win[0] == win[1] and win[1] == win[2] :
                self.who_win(win[0])
                self.win = True
                print('win')

    def not_win(self) :
        if self.turn == 9 and not self.win:
            no_win = pg.image.load('image/no_win.png').convert()
            self.win = True
            self.screen.blit(no_win, (0, 0))
            pg.time.delay(1000)
            pg.display.update()

    def choice(self) :
        x, y = self.getpos()
        self.set_xy(x, y)
        if self.search_board() :
            if self.choice_player == 'o' :
                if self.turn % 2 == 1 :
                    self.set_board('o')
                    self.screen.blit(self.circle, (x, y))
                else :
                    self.set_board('x')
                    self.screen.blit(self.x_photo, (x, y))

            else :
                if self.turn % 2 == 0 :
                    self.set_board('o')
                    self.screen.blit(self.circle, (x, y))
                else :
                    self.set_board('x')
                    self.screen.blit(self.x_photo, (x, y))

            pg.display.update()
            self.is_win()
            self.not_win()
            self.turn += 1
            print(x, y)

    def event(self) :
        running = True
        while running:
            for event in pg.event.get():
                if event.type == pg.KEYDOWN :
                    if event.key == pg.K_k and not self.start :
                        self.start = True
                        self.game_start()
      ###                  
#                    if event.type == pg.MOUSEBUTTONDOWN and not self.start:
#                        self.miss = True

                    if event.key == pg.K_o and self.start and not self.which_choice :
                        self.who_start('o')

                    if event.key == pg.K_x and self.start and not self.which_choice :
                        self.who_start('x')

                if event.type == pg.MOUSEBUTTONDOWN and self.start and self.which_choice and not self.win:
                    print('turn = ', self.turn)
                    self.choice()
                    print(self.board)

                if event.type == pg.QUIT :
                    pg.quit()

game = Game()
game.interface_gui()