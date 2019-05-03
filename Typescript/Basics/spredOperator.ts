 interface IState {
     home: number;
     away: number;
 }
 
 const initialState: IState = {
    home: 0,
    away: 0,
  };

  function reducer(state: IState = initialState): void {
      
    let obj = {
        ...state,
        home: state.home + 1,
    };

    console.log(obj);
}