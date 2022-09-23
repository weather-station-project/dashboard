import { dateFormatter } from '../../../../../components/pages/HistoricalData/Tables/BaseConfig';
import moment from 'moment';

describe('BaseConfig', () => {
  describe('BaseConfig', () => {
    it('When_GettingDateFormatted_Should_ReturnExpectedValue', () => {
      const date = new Date();
      expect(dateFormatter(date as never)).toEqual(moment(date).format('DD-MM-YYYY hh:mm:ss'));
    });
  });
});
